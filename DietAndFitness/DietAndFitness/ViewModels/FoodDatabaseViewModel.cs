using DietAndFitness.Core;
using DietAndFitness.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DietAndFitness.Controls;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using DietAndFitness.Validators;
using System.Timers;
using System.Threading.Tasks;
using DietAndFitness.Services;
using System.Runtime.CompilerServices;
using DietAndFitness.Views;
namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the Food Database page
    /// </summary>
    public class FoodDatabaseViewModel : ViewModelBase
    {
        #region Members
        private readonly IDialogService dialogService;
        private ObservableCollection<GlobalFoodItem> foodItems;
        private DataAccessLayer<GlobalFoodItem> DBAccess;
        private string progressindicator = "Waiting for input...";
        private GlobalFoodItem selectedItem;
        private RelayCommand confirmDeleteCommand;
        private NavigationService navigationService;
        #endregion
        #region Properties
        public ICommand OpenAddPageCommand { get; private set; }
        public ICommand OpenEditPageCommand { get; private set; }
        public GlobalFoodItem SelectedItem
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
        public ObservableCollection<GlobalFoodItem> FoodItems
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
        public RelayCommand ConfirmDeleteCommand
        {
            get
            {
                return confirmDeleteCommand
                       ?? (confirmDeleteCommand = new RelayCommand(
                           async () =>
                           {
                               await dialogService.ShowMessage("Are you sure you want to delete this item?",
                                  "Warning!",
                                  "Yes",
                                  "No",
                                   async (r) => {
                                       if (r == true)
                                       {
                                           if (SelectedItem != null)
                                               try
                                               {
                                                   await DBAccess.Delete(SelectedItem);
                                                   LoadList();
                                                   SelectedItem = null;
                                               }
                                               catch (Exception ex)
                                               {
                                                   await dialogService.ShowError(ex, "Error", "Ok", null);
                                               }
                                       }
                                   });
                           }, ValidateDeleteButton));
            }
        }

        #endregion
        public FoodDatabaseViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            dialogService = new DialogService();
            SelectedItem = new GlobalFoodItem();
            DBAccess = new DataAccessLayer<GlobalFoodItem>(GlobalSQLiteConnection.Database);
            OpenAddPageCommand = new Command(execute: OpenAddPageFunction);
            OpenEditPageCommand = new Command<GlobalFoodItem>(execute: OpenEditPageFunction, canExecute: ValidateEditButton);
            this.PropertyChanged += OnSelectedItemChanged;
        }
        #region Methods
        async void OpenAddPageFunction()
        {
            
            var addFoodItemPage = new AddFoodItemDB();
            addFoodItemPage.BindingContext = new AddFoodItemDBViewModel(navigationService);
            await navigationService.PushModal(addFoodItemPage);
            SelectedItem = null;
        }
        public async void LoadList()
        {

            FoodItems = new ObservableCollection<GlobalFoodItem>(await DBAccess.Get());

        }
        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }
        bool ValidateDeleteButton()
        {
            if (SelectedItem == null)
                return false;
            return true;
        }
        private async void OpenEditPageFunction(GlobalFoodItem parameter)
        {
            if (parameter != null)
            {
                var editFoodItemPage = new EditFoodItemDB();
                editFoodItemPage.BindingContext = new EditFoodItemDBViewModel(parameter, navigationService);
                await navigationService.PushModal(editFoodItemPage);
            }
            SelectedItem = null;
        }
        bool ValidateEditButton(GlobalFoodItem parameter)
        {
            if (parameter == null)
                return false;
            else
                return true;
        }
        void OnSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            (ConfirmDeleteCommand as RelayCommand).RaiseCanExecuteChanged();
        }
        #endregion
    }
}
