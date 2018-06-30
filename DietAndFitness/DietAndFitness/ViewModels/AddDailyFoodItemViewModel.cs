using DietAndFitness.Controls;
using DietAndFitness.Models;
using DietAndFitness.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class AddDailyFoodItemViewModel : AddFoodItemDBViewModel<DailyFoodItem>
    {
        private ObservableCollection<DatabaseEntity> foodItems;
        private DatabaseEntity selectedItem;
        private DataAccessLayer DBGlobalAccess;
        private DataAccessLayer DBFoodItemAccess;
        private string searchBarText;
        public string SearchBarText
        {
            get
            {
                return searchBarText;
            }
            set
            {
                if (searchBarText == value)
                    return;
                searchBarText = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<DatabaseEntity> FoodItems
        {
            get
            {
                return foodItems;
            }

            set
            {
                foodItems = value;
                OnPropertyChanged();

            }
        }
        public DatabaseEntity SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem == value)
                    return;
                selectedItem = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; private set; }
        public AddDailyFoodItemViewModel(NavigationService navigationService) : base(navigationService)
        {
            FoodItems = new ObservableCollection<DatabaseEntity>();
            DBGlobalAccess = new DataAccessLayer(GlobalSQLiteConnection.GlobalDatabase);
            DBFoodItemAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            SearchCommand = new Command<string>(execute: RefreshListItems);
            this.PropertyChanged += OnSelectedItemChanged;
            

        }

        private void OnSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            (AddCommand as Command).ChangeCanExecute();
        }
        protected override void Add(DailyFoodItem Parameter)
        {
            if (SelectedItem != null)
                ItemToAdd.SetValues(SelectedItem);
            base.Add(Parameter);
            SelectedItem = null;

        }
        /// <summary>
        /// Search method for finding an item to add. Highly inefficient as each serach selects the whole database
        /// TODO: Cache whole database? Custom selects?
        /// TODO: Create Behavior that can bind event to command for creating dynamically updated list
        /// </summary>
        /// <param name="parameter"></param>
        async void RefreshListItems(string parameter)
        {
            if (parameter != null)
            {
                List<LocalFoodItem> localFoodItems = new List<LocalFoodItem>();
                List<GlobalFoodItem> globalFoodItems = new List<GlobalFoodItem>();
                try
                {
                    globalFoodItems = await DBGlobalAccess.GetAllAsync<GlobalFoodItem>();
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
                try
                {
                    localFoodItems = await DBFoodItemAccess.GetAllAsync<LocalFoodItem>();
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
                FoodItems = new ObservableCollection<DatabaseEntity>();

                globalFoodItems = globalFoodItems.FindAll(delegate (GlobalFoodItem item)
                {
                    return item.Name.ToLower().Contains(parameter.ToLower());
                });
                localFoodItems = localFoodItems.FindAll(delegate (LocalFoodItem item)
                {
                    return item.Name.ToLower().Contains(parameter.ToLower());
                });
                foreach (var item in globalFoodItems)
                    FoodItems.Add(item);
                foreach (var item in localFoodItems)
                    FoodItems.Add(item);
                SelectedItem = null;
            }

        }
        protected override bool ValidateAddButton(DailyFoodItem parameter)
        {
            if (parameter == null || parameter.Quantity == 0 || SelectedItem==null)
                return false;
            return true;
        }
      

    }
}
