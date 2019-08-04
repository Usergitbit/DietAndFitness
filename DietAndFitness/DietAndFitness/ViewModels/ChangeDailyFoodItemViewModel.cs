using DietAndFitness.Controls;
using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using DietAndFitness.Services;
using DietAndFitness.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (foodItems == value)
                    return;
                foodItems = value;
                OnPropertyChanged();
            }
        }

        private IEnumerable<object> _filteredItems;
        public IEnumerable<object> FilteredItems
        {
            get { return _filteredItems; }
            set
            {
                if (_filteredItems == value)
                    return;
                _filteredItems = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; private set; }
        public ChangeDailyFoodItemViewModel( ) : base()
        {
            Initialize();
        }
        public ChangeDailyFoodItemViewModel(DateTime date) : base(date)
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

        private LocalFoodItem _selectedFilterItem;
        public LocalFoodItem selectedFilterItem
        {
            get { return _selectedFilterItem; }
            set
            {
                if (_selectedFilterItem == value)
                    return;
                _selectedFilterItem = value;
                OnPropertyChanged();
            }
        }
        private void Initialize()
        {
            FoodItems = new ObservableCollection<LocalFoodItem>();
            FilteredItems = new ObservableCollection<LocalFoodItem>();
            SearchCommand = new Command<string>(execute: RefreshListItems);
            PropertyChanged += OnSelectedItemChanged;
            //CurrentItem.PropertyChanged += OnSelectedItemChanged;

        }

        public async Task LoadList()
        {
            FoodItems = new ObservableCollection<LocalFoodItem> (await DBLocalAccess.GetAllAsync<LocalFoodItem>());
        }
        private void OnSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            (ExecuteOperationCommand as Command).ChangeCanExecute();
            //This is so the Edit button will become available when the SelectedFoodItem changes
            if(CurrentItem != null)
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
        /// Search method for finding an item to add. 
        /// </summary>
        /// <param name="parameter"></param>
        async void RefreshListItems(string parameter)
        {
            if (parameter != null)
            {
                FoodItems = new ObservableCollection<LocalFoodItem>(await DBLocalAccess.GetByDescription(parameter));
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

        public override void Dispose()
        {
            PropertyChanged -= OnSelectedItemChanged;
            base.Dispose();
        }
    }
}
