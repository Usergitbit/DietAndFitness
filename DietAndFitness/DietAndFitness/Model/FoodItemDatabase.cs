using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using SQLite;

namespace DietAndFitness.Model
{
    public class FoodItemDatabase
    {
        private SQLiteAsyncConnection database;

        public FoodItemDatabase(string DatabasePath)
        {
            database = new SQLiteAsyncConnection(DatabasePath);
            database.CreateTableAsync<FoodItem>().Wait();
        }
    }
}
