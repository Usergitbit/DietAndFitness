using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces;
using DietAndFitness.Interfaces.Repositories;
using DietAndFitness.Services;
using DietAndFitness.Services.Repositories;
using EFCore.BulkExtensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietAndFitness.Controls
{
    public class DataAccessService : IDataAccessService
    {
        private static readonly SQLiteDbContext sqliteDbContext = new SQLiteDbContext();

        /// <summary>
        /// Will close the file stream!
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task ExportData(ICrossFile crossFile)
        {

            await using var file = crossFile;
            using var stream = file.Stream;
            //create an archive from that stream
            using var archive = new ZipArchive(stream, ZipArchiveMode.Create, false);
            //create a file in the archive and copy the data into it
            var profileTypesFile = archive.CreateEntry("ProfileTypes.json");

            using (var archiveFileStream = profileTypesFile.Open())
            using (var streamWriter = new StreamWriter(archiveFileStream))
            {
                var profileTypes = await sqliteDbContext.ProfileTypes.AsNoTracking().ToListAsync();
                var profileTypesJson = JsonConvert.SerializeObject(profileTypes);
                await streamWriter.WriteAsync(profileTypesJson);
            }

            var dietFormulasFile = archive.CreateEntry("DietFormulas.json");
            using (var archiveFileStream = dietFormulasFile.Open())
            using (var streamWriter = new StreamWriter(archiveFileStream))
            {
                var profileTypes = await sqliteDbContext.DietFormulas.AsNoTracking().ToListAsync();
                var profileTypesJson = JsonConvert.SerializeObject(profileTypes);
                await streamWriter.WriteAsync(profileTypesJson);
            }

            var profilesFile = archive.CreateEntry("Profiles.json");
            using (var archiveFileStream = profilesFile.Open())
            using (var streamWriter = new StreamWriter(archiveFileStream))
            {
                var profiles = await sqliteDbContext.Profiles.AsNoTracking().ToListAsync();
                var profilesJson = JsonConvert.SerializeObject(profiles);
                await streamWriter.WriteAsync(profilesJson);
            }

            var localFoodItemsFile = archive.CreateEntry("LocalFoodItems.json");
            using (var archiveFileStream = localFoodItemsFile.Open())
            using (var streamWriter = new StreamWriter(archiveFileStream))
            {
                var localFoodItems = await sqliteDbContext.LocalFoodItems.AsNoTracking().ToListAsync();
                var localFoodItemsJson = JsonConvert.SerializeObject(localFoodItems);
                await streamWriter.WriteAsync(localFoodItemsJson);
            }

            var dailyFoodItemsFile = archive.CreateEntry("DailyFoodItems.json");
            using (var archiveFileStream = dailyFoodItemsFile.Open())
            using (var streamWriter = new StreamWriter(archiveFileStream))
            {
                var dailyFoodItems = await sqliteDbContext.DailyFoodItems.AsNoTracking().ToListAsync();
                var dailyFoodItemsJson = JsonConvert.SerializeObject(dailyFoodItems);
                await streamWriter.WriteAsync(dailyFoodItemsJson);
            }
        }
        public async Task ImportData(ICrossFile crossFile)
        {
            await using var file = crossFile;

            using var stream = file.Stream;
            using var archive = new ZipArchive(stream, ZipArchiveMode.Read, false);

            var connection = (SqliteConnection)sqliteDbContext.Database.GetDbConnection();

            var entityFrameworkTransaction = await sqliteDbContext.Database.BeginTransactionAsync();
            try
            {
                var sqliteTransaction = (SqliteTransaction)entityFrameworkTransaction.GetUnderlyingTransaction(new BulkConfig());

                using var transaction = sqliteTransaction;
                var bulkConfig = new BulkConfig()
                {
                    UnderlyingConnection = (param) => connection,
                    UnderlyingTransaction = (param) => transaction
                };

                await DeleteAllData(Device.RuntimePlatform);

                await InsertAllData(archive, bulkConfig);

                await sqliteDbContext.SaveChangesAsync();
                await entityFrameworkTransaction.CommitAsync();
            }
            catch(Exception ex)
            {
                await entityFrameworkTransaction.RollbackAsync();
                throw;
            }

        }
        private async Task DeleteAllData(string device)
        {
            switch (device)
            {
                case Device.UWP:
                    var dailyFoodItemsToDelete = await sqliteDbContext.DailyFoodItems.ToArrayAsync();
                    sqliteDbContext.DailyFoodItems.RemoveRange(dailyFoodItemsToDelete);
                    await sqliteDbContext.SaveChangesAsync();

                    var localFoodItemsToDelete = await sqliteDbContext.LocalFoodItems.ToArrayAsync();
                    sqliteDbContext.LocalFoodItems.RemoveRange(localFoodItemsToDelete);
                    await sqliteDbContext.SaveChangesAsync();

                    var profilesToDelete = await sqliteDbContext.Profiles.ToArrayAsync();
                    sqliteDbContext.Profiles.RemoveRange(profilesToDelete);
                    await sqliteDbContext.SaveChangesAsync();

                    var profileTypesToDelete = await sqliteDbContext.ProfileTypes.ToArrayAsync();
                    sqliteDbContext.ProfileTypes.RemoveRange(profileTypesToDelete);
                    await sqliteDbContext.SaveChangesAsync();

                    var dietForumaslToDelete = await sqliteDbContext.DietFormulas.ToArrayAsync();
                    sqliteDbContext.DietFormulas.RemoveRange(dietForumaslToDelete);
                    await sqliteDbContext.SaveChangesAsync();
                    break;
                default:
                    await sqliteDbContext.DailyFoodItems.BatchDeleteAsync();
                    await sqliteDbContext.LocalFoodItems.BatchDeleteAsync();
                    await sqliteDbContext.Profiles.BatchDeleteAsync();
                    await sqliteDbContext.DietFormulas.BatchDeleteAsync();
                    await sqliteDbContext.ProfileTypes.BatchDeleteAsync();
                    await sqliteDbContext.SaveChangesAsync();
                    break;
            }
        }
        private async Task InsertAllData(ZipArchive archive, BulkConfig bulkConfig)
        {
            var profileTypesFile = archive.GetEntry("ProfileTypes.json");
            using (var archiveFileStream = profileTypesFile.Open())
            using (var streamReader = new StreamReader(archiveFileStream))
            {
                var profileTypesJson = await streamReader.ReadToEndAsync();
                var profileTypes = JsonConvert.DeserializeObject<List<ProfileType>>(profileTypesJson);
                await sqliteDbContext.BulkInsertAsync(profileTypes, bulkConfig);
                await sqliteDbContext.SaveChangesAsync();
            }

            var dietFormulasFile = archive.GetEntry("DietFormulas.json");
            using (var archiveFileStream = dietFormulasFile.Open())
            using (var streamReader = new StreamReader(archiveFileStream))
            {
                var dietFormulasJson = await streamReader.ReadToEndAsync();
                var dietFormulas = JsonConvert.DeserializeObject<List<DietFormula>>(dietFormulasJson);
                await sqliteDbContext.BulkInsertAsync(dietFormulas, bulkConfig);
                await sqliteDbContext.SaveChangesAsync();
            }

            var profilesFile = archive.GetEntry("Profiles.json");
            using (var archiveFileStream = profilesFile.Open())
            using (var streamReader = new StreamReader(archiveFileStream))
            {
                var profilesJson = await streamReader.ReadToEndAsync();
                var profiles = JsonConvert.DeserializeObject<List<Profile>>(profilesJson);
                await sqliteDbContext.BulkInsertAsync(profiles, bulkConfig);
                await sqliteDbContext.SaveChangesAsync();
            }

            var localFoodItemsFile = archive.GetEntry("LocalFoodItems.json");
            using (var archiveFileStream = localFoodItemsFile.Open())
            using (var streamReader = new StreamReader(archiveFileStream))
            {
                var localFoodItemsJson = await streamReader.ReadToEndAsync();
                var localFoodItems = JsonConvert.DeserializeObject<List<LocalFoodItem>>(localFoodItemsJson);
                await sqliteDbContext.BulkInsertAsync(localFoodItems, bulkConfig);
                await sqliteDbContext.SaveChangesAsync();
            }

            var dailyFoodItemsFile = archive.GetEntry("DailyFoodItems.json");
            using (var archiveFileStream = dailyFoodItemsFile.Open())
            using (var streamReader = new StreamReader(archiveFileStream))
            {
                var dailyFoodItemsJson = await streamReader.ReadToEndAsync();
                var dailyFoodItems = JsonConvert.DeserializeObject<List<DailyFoodItem>>(dailyFoodItemsJson);
                await sqliteDbContext.BulkInsertAsync(dailyFoodItems, bulkConfig);
                await sqliteDbContext.SaveChangesAsync();
            }
        }

        public IDailyFoodItemsRepository DailyFoodItems { get; }
        public ILocalFoodItemsRepository LocalFoodItems { get; }
        public IProfileTypesRepository ProfileTypes { get; }
        public IDietFormulasRepository DietFormulas { get; }
        public IUserProfilesRepository UserProfiles { get;}

        public DataAccessService(IDailyFoodItemsRepository dailyFoodItemsRepository, 
            ILocalFoodItemsRepository localFoodItemsRepository, 
            IUserProfilesRepository userProfileRepository,
            IDietFormulasRepository dietFormulaRepository,
            IProfileTypesRepository profileTypesRepository)
        {
            DailyFoodItems = dailyFoodItemsRepository;
            LocalFoodItems = localFoodItemsRepository;
            UserProfiles = userProfileRepository;
            DietFormulas = dietFormulaRepository;
            ProfileTypes = profileTypesRepository;
        }

    }
}
