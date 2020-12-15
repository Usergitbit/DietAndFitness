using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using DietAndFitness.Extensions;
using DietAndFitness.Interfaces;
using DietAndFitness.Services.Repositories;
using DietAndFitness.ViewModels.Base;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class ChangeDailyFoodItemViewModel : ChangeBaseViewModel<DailyFoodItem>
    {
        private readonly DebounceDispatcher debounceDispatcher;
        private List<LocalFoodItem> foodItems;
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
                debounceDispatcher.Debounce(500, async () =>
                {
                    await FilterItems();
                });
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
                CurrentItem.Name = SelectedFoodItem?.Name;
                OnPropertyChanged();
            }
        }
        public List<LocalFoodItem> FoodItems
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

        private ObservableCollection<LocalFoodItem> _filteredItems;
        public ObservableCollection<LocalFoodItem> FilteredItems
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

        private DateTime currentDate;
        public ChangeDailyFoodItemViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            FoodItems = new List<LocalFoodItem>();
            FilteredItems = new ObservableCollection<LocalFoodItem>();
            PropertyChanged += OnSelectedItemChanged;
            debounceDispatcher = new DebounceDispatcher();
        }
        private LocalFoodItem _selectedFilterItem;
        public LocalFoodItem SelectedFilterItem
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

        public override async Task InitializeAsync(params object[] parameters)
        {
            var currentItemId = (int?)parameters.ElementAtOrDefault(0);
            currentDate = (DateTime)parameters.ElementAtOrDefault(1);
            if (currentItemId != null)
            {
                CurrentItem = await DBLocalAccess.DailyFoodItems.GetAsync(dfi => dfi.ID == currentItemId);
                OperationInfo = "Edit";
            }
        }


        private async Task<IEnumerable<LocalFoodItem>> RecentlyAddedItems()
        {
            var recentlyAddedItems = await DBLocalAccess.LocalFoodItems.GetRecentlyAddedItems();
            return recentlyAddedItems;
        }
        public async Task LoadList()
        {
            FoodItems = await DBLocalAccess.LocalFoodItems.GetAllAsync();
            await FilterItems();
        }
        private void OnSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            (ExecuteOperationCommand as Command).ChangeCanExecute();
            //This is so the Edit button will become available when the SelectedFoodItem changes
            if (CurrentItem != null)
                CurrentItem.IsDirty = true;
        }
        public async Task FilterItems()
        {
            IEnumerable<LocalFoodItem> filteredItems = null;
            await Task.Run(async () =>
            {
                if (string.IsNullOrWhiteSpace(SearchBarText))
                    filteredItems = await RecentlyAddedItems();
                else
                    filteredItems = FoodItems.Where(fi => fi.Name.ToLower().Contains(SearchBarText.ToLower()));
            });
            FilteredItems = new ObservableCollection<LocalFoodItem>(filteredItems);
        }
        /// <summary>
        /// Override to set the selected LocalFoodItemID and Name and reset the selected LocalFoodItem
        /// </summary>
        /// <param name="parameter"></param>
        protected override async Task Operation(DailyFoodItem parameter)
        {
            if (CurrentItem.ID == null)
                CurrentItem.CreatedAt = currentDate;
            else
                CurrentItem.ModifiedAt = currentDate;
            //await base.Operation(parameter);
            if (parameter.ID == null)
            {

                if (parameter.IsValid())
                    try
                    {
                        var dailyFoodItem = parameter;
                        await DBLocalAccess.DailyFoodItems.InsertAsync(parameter);
                        //if program stays here the insert date will be incorrect
                        // we lose the reference when we make a new item so we have to unsubscribe here to prevent memory leaks
                        CurrentItem.PropertyChanged -= OnCurrentItemPropertyChanged;
                        CurrentItem = new DailyFoodItem(currentDate);
                        //resubscribe for the new item
                        CurrentItem.PropertyChanged += OnCurrentItemPropertyChanged;
                        OnOperationSuccess();
                    }
                    catch (Exception ex)
                    {
                        await dialogService.ShowError(ex, "Error", "Ok", null);
                        OnOperationFailiure();
                    }
            }
            else
            {
                if (parameter.IsValid())
                    try
                    {
                        await DBLocalAccess.DailyFoodItems.UpdateAsync(parameter);
                        //if program stays here the insert date will be incorrect
                        CurrentItem.Clean();
                        (ExecuteOperationCommand as Command).ChangeCanExecute();
                        OnOperationSuccess();
                    }
                    catch (Exception ex)
                    {
                        await dialogService.ShowError(ex, "Error", "Ok", null);
                        OnOperationFailiure();
                    }
            }
        }

        private void OnCurrentItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            (ExecuteOperationCommand as Command).ChangeCanExecute();
        }

        protected override bool ValidateExecuteOperationButton(DailyFoodItem parameter)
        {

            if (parameter == null || parameter.Quantity == 0 || CurrentItem.FoodItem == null)
                return false;
            else
                if (parameter.ID != null && !parameter.IsDirty)
                return false;
            return true;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
