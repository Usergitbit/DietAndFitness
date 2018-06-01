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
        public static SQLiteAsyncConnection Database { get; private set; }
        public static void ConnectToDatabaseAsync(string DatabasePath)
        {
            try
            {
                Database = new SQLiteAsyncConnection(DatabasePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine( "Error at accessing database file" + e.Message );
            }
        }
    }
}
