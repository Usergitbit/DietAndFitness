using DietAndFitness.Core;
using DietAndFitness.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DietAndFitness.Controls;
using System.Windows.Input;
using Xamarin.Forms;
/// <summary>
/// ViewModel for the Food Database Page
/// </summary>
namespace DietAndFitness.ViewModels
{
    public class FoodDatabaseViewModel : ViewModelBase
    {
        public ICommand AddCommand { get; private set; }

        private ObservableCollection<GlobalFoodItem> fooditems;
        private DataAccessLayer<GlobalFoodItem> DBAccess;

        public ObservableCollection<GlobalFoodItem> FoodItems
        {
            get
            {
                return fooditems;
            }

            set
            {
                fooditems = value;
                OnPropertyChanged();
            }
        }
        public FoodDatabaseViewModel()
        {
            DBAccess = new DataAccessLayer<GlobalFoodItem>(GlobalSQLiteConnection.Database);
            AddCommand = new Command(Add);
        }

        public async void LoadList()
        {

            FoodItems = new ObservableCollection<GlobalFoodItem>(await DBAccess.Get());

        }

        public async void Add()
        {
            //FoodItems.Add(new GlobalFoodItem("Item"));
            await DBAccess.Insert(new GlobalFoodItem("Item","100","1488","20","14","88","prahit",false));
            LoadList();
        }

    }
}
