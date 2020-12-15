using System.Threading.Tasks;
using DietAndFitness.Views;
using DietAndFitness.ViewModels.Base;
using DietAndFitness.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using DietAndFitness.Extensions;
using DietAndFitness.Interfaces;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the Food Database page
    /// </summary>
    public class FoodDatabaseViewModel : ListBaseViewModel<LocalFoodItem>
    {
        #region Members
        private string progressindicator = "Waiting for input...";
        #endregion
        #region Properties
        public string ProgressIndicator
        {
            get
            {
                return progressindicator;
            }

            set
            {
                if (progressindicator == value)
                    return;
                progressindicator = value;
                OnPropertyChanged();
            }
        }


        private string _SearachText;
        public string SearchText
        {
            get { return _SearachText; }
            set
            {
                if (_SearachText == value)
                    return;
                _SearachText = value;
                debounceDispatcher.Debounce(500, async () =>
                {
                    await FilterItems();
                });
                OnPropertyChanged();
            }
        }


        private ObservableCollection<LocalFoodItem> _FilteredItems;
        public ObservableCollection<LocalFoodItem> FilteredItems
        {
            get { return _FilteredItems; }
            set
            {
                if (_FilteredItems == value)
                    return;
                _FilteredItems = value;
                OnPropertyChanged();
            }
        }

        private readonly DebounceDispatcher debounceDispatcher;
        #endregion
        public FoodDatabaseViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            debounceDispatcher = new DebounceDispatcher();
            FilteredItems = new ObservableCollection<LocalFoodItem>();
        }
        #region Methods

        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }

        private async Task FilterItems()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {

                FilteredItems.Clear();
                foreach (var item in Items)
                {

                    FilteredItems.Add(item);
                    await Task.Delay(10);

                }

            }
            else
                FilteredItems = new ObservableCollection<LocalFoodItem>(Items.Where(fi => fi.Name.ToLower().Contains(SearchText.ToLower())));
        }

        public override async Task LoadList()
        {
            try
            {
                var localFoodItems = await DBLocalAccess.LocalFoodItems.GetAllAsync();
                Items = new ObservableCollection<LocalFoodItem>(localFoodItems);
            }
            catch (Exception ex)
            {
                await dialogService.ShowError(ex, "Error", "Ok", null);
            }
            await FilterItems();
        }

        protected override async Task OpenAddPageFunction()
        {
            await navigationService.PushModal("ChangeFoodItemDB");
        }

        protected override async Task OpenEditPageFunction(LocalFoodItem parameter)
        {
            await navigationService.PushModal("ChangeFoodItemDB", SelectedItem.ID);
        }

        protected override async Task ExecuteDelete(bool result)
        {
            if (result == true)
            {
                try
                {
                    await DBLocalAccess.LocalFoodItems.DeleteAsync(SelectedItem);
                    await LoadList();
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
            };
        }
        #endregion
    }
}
