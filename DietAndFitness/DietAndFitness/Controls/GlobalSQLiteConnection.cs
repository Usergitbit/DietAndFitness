using Microsoft.Data.Sqlite;
using SQLite;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading.Tasks;
/// <summary>
/// Static class that provides access to the database from anywhere in the program
/// Should only be used to instantiate a DataAccessLayer
/// </summary>
namespace DietAndFitness.Controls
{
    /// <summary>
    /// Static class that provides access to the database from anywhere in the program
    /// Should only be used to instantiate a DataAccessLayer
    /// </summary>
    public static class GlobalSQLiteConnection
    {
        public static SQLiteAsyncConnection GlobalDatabase { get; private set; }
        public static SQLiteAsyncConnection LocalDatabase { get; private set; }
        public static SQLiteConnection LocaDataBaseSync { get; private set; }
        public static void ConnectToGlobalDatabaseAsync(string DatabasePath)
        {
            try
            {
                //False attribute indicates dates should be stored normally and NOT as integer values 
                GlobalDatabase = new SQLiteAsyncConnection(DatabasePath,false);
            }
            catch (Exception e)
            {
                Debug.WriteLine( "Error at accessing database file" + e.Message );
            }
        }
        public static void ConnectToLocalDatabaseAsync(string DatabasePath)
        {
            try
            {
                LocalDatabase = new SQLiteAsyncConnection(DatabasePath, false);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error at accessing database file" + e.Message);
            }
        }
        public static void ConnectToLocalDatabase(string DatabasePath)
        {
            try
            {
                LocaDataBaseSync = new SQLiteConnection(DatabasePath, false);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error at accessing database file" + e.Message);
            }
        }

        public static async Task<DbConnection> GetSQLiteConnection(string databasePath)
        {
            var connection = new SqliteConnection("" + new SqliteConnectionStringBuilder { DataSource = databasePath });
            await connection.OpenAsync();
            return connection;
        }
    }
}
