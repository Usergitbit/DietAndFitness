using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DietAndFitness.Model
{
    public class FoodItemDatabase
    {
        private SQLiteAsyncConnection database;

        public FoodItemDatabase(string DatabasePath)
        {
            try
            {
                database = new SQLiteAsyncConnection(DatabasePath);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message + " Error at connecting to the database");
            }
        }

        public async Task<List<FoodItem>> Get()
        {
            Debug.WriteLine("Code before tabel querry reached");
            var list =  await database.Table<FoodItem>().ToListAsync();
            Debug.WriteLine("code after table querry reached");

            return list;
        }
    }
}
