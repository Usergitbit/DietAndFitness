using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SQLite;
namespace DietAndFitness.Model
{
    public static class SQLiteConnection
    {
        private static SQLiteAsyncConnection database = null;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (database.Equals(null))
                {
                    Debug.WriteLine("NO CONNECTION TO THE DATABASE");
                    return null;
                }

                return database;
            }
        }
        public static void ConnectAsync(string DatabasePath)
        {
            database = new SQLiteAsyncConnection(DatabasePath);
        }
    }
}
