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
    public class FoodDatabaseViewModel<T> : ViewModelBase where T: DatabaseEntity, new()
    {
        #region Members
        private readonly IDialogService dialogService;
        private ObservableCollection<DatabaseEntity> foodItems;
        private DataAccessLayer<GlobalFoodItem> DBGlobalAccess;
        protected DataAccessLayer<T> DBLocalAccess;
        private string progressindicator = "Waiting for input...";
        private DatabaseEntity selectedItem;
        private RelayCommand confirmDeleteCommand;
        protected NavigationService navigationService;
        #endregion
        #region Properties
        public ICommand OpenAddPageCommand { get; private set; }
        public ICommand OpenEditPageCommand { get; private set; }
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
                                                   
                                                   await DBLocalAccess.Delete((T)SelectedItem);
                                                   LoadList();
                                                   SelectedItem = null;
                                               }
                                               catch (Exception ex)
                                               {
                                                   await dialogService.ShowError(ex, "Error", "Ok", null);
                                               }
                                           else
                                               {
                                                   await dialogService.ShowMessage("Preset food items cannot be deleted","Error");
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
            //SelectedItem = new ModelBase();
            FoodItems = new ObservableCollection<DatabaseEntity>();
            DBGlobalAccess = new DataAccessLayer<GlobalFoodItem>(GlobalSQLiteConnection.GlobalDatabase);
            DBLocalAccess = new DataAccessLayer<T>(GlobalSQLiteConnection.LocalDatabase);
            OpenAddPageCommand = new Command(execute: OpenAddPageFunction);
            OpenEditPageCommand = new Command<T>(execute: OpenEditPageFunction, canExecute: ValidateEditButton);
            this.PropertyChanged += OnSelectedItemChanged;
            
        }
        #region Methods
        protected virtual async void OpenAddPageFunction()
        {
            
            var addFoodItemPage = new AddFoodItemDB();
            addFoodItemPage.BindingContext = new AddFoodItemDBViewModel<T>(navigationService);
            await navigationService.PushModal(addFoodItemPage);
            SelectedItem = null;
        }
        public virtual async void LoadList()
        {
            List<T> localFoodItems = new List<T>();
            List<GlobalFoodItem> globalFoodItems = new List<GlobalFoodItem>();
            try
            {
                globalFoodItems =  await DBGlobalAccess.GetAll();
            }
            catch(Exception ex)
            {
                await dialogService.ShowError(ex, "Error", "Ok",null);
            }
            try
            {
                localFoodItems = await DBLocalAccess.GetAll();
            }
            catch (Exception ex)
            {
                await dialogService.ShowError(ex, "Error", "Ok", null);
            }
            FoodItems = new ObservableCollection<DatabaseEntity>();
            foreach (var item in globalFoodItems)
                FoodItems.Add(item);
            foreach (var item in localFoodItems)
                FoodItems.Add(item);

        }
        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }
        bool ValidateDeleteButton()
        {
            if (SelectedItem == null || SelectedItem.GetType() == typeof(GlobalFoodItem))
                return false;
            return true;
        }
        protected virtual async void OpenEditPageFunction(T parameter)
        {
            if (parameter != null)
            {
                var editFoodItemPage = new EditFoodItemDB();
                editFoodItemPage.BindingContext = new EditFoodItemDBViewModel<T>(parameter, navigationService);
                await navigationService.PushModal(editFoodItemPage);
            }
            SelectedItem = null;
        }
        bool ValidateEditButton(T parameter)
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
