using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
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
        public static void ConnectToGlobalDatabaseAsync(string DatabasePath)
        {
            try
            {
                GlobalDatabase = new SQLiteAsyncConnection(DatabasePath);
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
                LocalDatabase = new SQLiteAsyncConnection(DatabasePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error at accessing database file" + e.Message);
            }
        }
    }
}
