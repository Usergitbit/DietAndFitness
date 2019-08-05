using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace DietAndFitness.Controls
{
    public class CompatbilityManager
    {
        private IDataAccessService dataAccessService;
        private int versionNumber;
        public CompatbilityManager(IDataAccessService dataAccessService)
        {
            this.dataAccessService = dataAccessService;
        }

        public async void EnsureCompatibility()
        {
            try
            {
                versionNumber = (await dataAccessService.GetVersionAsync())[0].Number;
            }
            catch (Exception ex)
            {
                throw new Exception("Compatibility error: Failed to get current version! " + ex.Message);
            }
            switch (versionNumber)
            {
                case 0:
                    Update_0_1();
                    break;
            }
        }

        private void Update_0_1()
        {
            try
            {
                UpdateDates();
                UpdateProfileColumnName();
                dataAccessService.IncrementVersion();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update from version 0 to 1! " + ex.Message);
            }
        }


        /// <summary>
        /// Updates the column name from DietFormula to DietFormulaId so EF can create objects properly
        /// </summary>
        /// <returns></returns>
        private void UpdateProfileColumnName()
        {
            try
            {
                var sqliteContext = new SQLiteDbContext();
                var query = @"PRAGMA foreign_keys=off;

BEGIN TRANSACTION;

CREATE TABLE Profiles_new (
    ID             INTEGER PRIMARY KEY AUTOINCREMENT
                           NOT NULL,
    Name           STRING  NOT NULL,
    CreatedAt      DATE    NOT NULL,
    ModifiedAt     DATE    NOT NULL,
    Deleted        BOOLEAN NOT NULL,
    Height         DOUBLE  NOT NULL,
    Weight         DOUBLE  NOT NULL,
    BirthDate      DATE    NOT NULL,
    Sex            STRING  NOT NULL,
    DietFormulaId    INTEGER REFERENCES DietFormulas (ID) 
                           NOT NULL,
    ActivityLevel  DOUBLE  NOT NULL,
    BodyFat        DOUBLE,
    GUID           TEXT    UNIQUE,
    StartDate      DATE    NOT NULL,
    EndDate        DATE    NOT NULL,
    ProfileTypesId TEXT    REFERENCES ProfileTypes (ID) 
                           NOT NULL
);

INSERT INTO Profiles_new (ID, Name, CreatedAt, ModifiedAt, Deleted, Height, Weight, BirthDate, Sex, DietFormulaId, ActivityLevel, BodyFat, GUID, StartDate, EndDate, ProfileTypesId)
  SELECT ID, Name, CreatedAt, ModifiedAt, Deleted, Height, Weight, BirthDate, Sex, DietFormula, ActivityLevel, BodyFat, GUID, StartDate, EndDate, ProfileTypesId
  FROM Profiles;

drop table Profiles;

ALTER TABLE Profiles_new RENAME TO Profiles;


CREATE TRIGGER GENERATE_GUID_PROFILES
         AFTER INSERT
            ON Profiles
          WHEN new.GUID IS NULL
BEGIN
    UPDATE Profiles
       SET GUID = (
               SELECT '{' || hex(randomblob(4) ) || '-' || hex(randomblob(2) ) || '-' || '4' || substr(hex(randomblob(2) ), 2) || '-' || substr('AB89', 1 + (abs(random() ) % 4), 1) || substr(hex(randomblob(2) ), 2) || '-' || hex(randomblob(6) ) || '}'
           )
     WHERE id = NEW.ID;
END;

COMMIT;

PRAGMA foreign_keys=on;";
                var commnad = sqliteContext.Database.GetDbConnection().CreateCommand();
                sqliteContext.Database.OpenConnection();
                commnad.CommandText = query;
                commnad.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in UpdateProfileColumnName: " + ex.Message);
            }
        }

        /// <summary>
        /// This method updates dates from the previous weird format of 'yyyy-mm-ddThh:mm:ss' to not have the 'T' so that EF can parse it normally
        /// </summary>
        private void UpdateDates()
        {
            try
            {
                var sqliteContext = new SQLiteDbContext();
                sqliteContext.Database.BeginTransaction();
                var query = @"update dailyfooditem
                            set createdat = datetime(createdAt),
                            modifiedat = datetime(modifiedAt);
                          update dietFormulas
                            set createdat = date('now'),
                            modifiedat = date('now');
                          update localFoodItem
                            set createdat = datetime(createdAt),
                            modifiedat = datetime(modifiedAt);
                          update Profiles
                            set createdat = datetime(createdAt),
                            modifiedat = datetime(modifiedAt),
                            startDate = datetime(startDate),
                            endDate = datetime(endDate),
                            birthDate = datetime(birthDate);
                          update profileTypes
                            set createdat = datetime(createdAt),
                            modifiedat = datetime(modifiedAt); 
                          update ProfileTypes
                            set createdAt = datetime('now'),
                            modifiedAt = datetime('now'),
                            deleted = false;";
                sqliteContext.Database.ExecuteSqlCommand(query);
                sqliteContext.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception in UpdateDates: " + ex.Message);
            }
        }
    }
}
