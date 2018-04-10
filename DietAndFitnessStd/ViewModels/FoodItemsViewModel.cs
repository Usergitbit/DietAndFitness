using DietAndFitness.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DietAndFitness.ViewModels
{
    public class FoodItemsViewModel 
    {
        private SQLiteAsyncConnection database;


        public ObservableCollection<GlobalFoodItem> FoodItems { get; set; }


        public FoodItemsViewModel(SQLiteAsyncConnection _database)
        {
            database = _database;
          
        }

        public async void LoadList()
        {

                FoodItems = new ObservableCollection<GlobalFoodItem>(await database.Table<GlobalFoodItem>().ToListAsync());
 
        }

        public void Add()
        {
            FoodItems.Add(new GlobalFoodItem("Item"));

        }

    }
}
