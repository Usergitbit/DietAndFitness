using DietAndFitness.Controls;
using DietAndFitness.Entities;
using DietAndFitness.Services;
using DietAndFitness.ViewModels.Base;
using DietAndFitness.ViewModels.Secondary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class ChangeDailyFoodItemViewModel : ChangeBaseViewModel<DailyFoodItem>
    {
        private ObservableCollection<LocalFoodItem> foodItems;
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
        private LocalFoodItem selectedFoodItem;
        public LocalFoodItem SelectedFoodItem
        {
            get
            {
                return selectedFoodItem;
            }
            set
            {
                if (value == selectedFoodItem)
                    return;
                selectedFoodItem = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<LocalFoodItem> FoodItems
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
        public ICommand SearchCommand { get; private set; }
        public ChangeDailyFoodItemViewModel( ) : base()
        {
            Initialize();
        }
        public ChangeDailyFoodItemViewModel(CompleteFoodItem selectedItem) : base(selectedItem.DailyFoodItem)
        {
            Initialize();
            FoodItems.Add(selectedItem.LocalFoodItem);
            //Using the field instead of the property prevents the OnPropertyChanged event from firing and making the Edit button available from the start
            selectedFoodItem = selectedItem.LocalFoodItem;
        }
        private void Initialize()
        {
            FoodItems = new ObservableCollection<LocalFoodItem>();
            SearchCommand = new Command<string>(execute: RefreshListItems);
            PropertyChanged += OnSelectedItemChanged;
        }
        private void OnSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            (ExecuteOperationCommand as Command).ChangeCanExecute();
            //This is so the Edit button will become available when the SelectedFoodItem changes
            CurrentItem.IsDirty = true;
        }
        /// <summary>
        /// Override to set the selected LocalFoodItemID and Name and reset the selected LocalFoodItem
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Operation(DailyFoodItem parameter)
        {
            CurrentItem.FoodItemID = SelectedFoodItem.ID;
            CurrentItem.Name = SelectedFoodItem.Name;
            base.Operation(parameter);
            SelectedFoodItem = null;
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
                try
                {
                    localFoodItems = await DBLocalAccess.GetAllAsync<LocalFoodItem>();
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
                FoodItems = new ObservableCollection<LocalFoodItem>();
                localFoodItems = localFoodItems.FindAll(delegate (LocalFoodItem item)
                {
                    return item.Name.ToLower().Contains(parameter.ToLower());
                });
                foreach (var item in localFoodItems)
                    FoodItems.Add(item);
                SelectedFoodItem = null;
            }
        }
        protected override bool ValidateExecuteOperationButton(DailyFoodItem parameter)
        {

            if (parameter == null || parameter.Quantity == 0 || SelectedFoodItem == null)
                return false;
            else
                if (parameter.ID != null && !parameter.IsDirty)
                    return false;
            return true;
        }
    }
}
