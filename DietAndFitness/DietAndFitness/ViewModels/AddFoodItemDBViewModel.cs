using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Models;
using DietAndFitness.Services;
using DietAndFitness.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the AddFoodItemDB page
    /// </summary>
    public class AddFoodItemDBViewModel : ViewModelBase
    {
        #region Members
        private LocalFoodItem itemToAdd;
        private readonly IDialogService dialogService;
        private string progressindicator = "Waiting for input...";
        private NavigationService navigationService;
        private DataAccessLayer<LocalFoodItem> DBLocalAccess;
        #endregion
        #region Properties
        public LocalFoodItem ItemToAdd
        {
            get
            {
                return itemToAdd;
            }

            set
            {
                if (value == itemToAdd)
                    return;
                itemToAdd = value;
                OnPropertyChanged();

            }
        }
        public ICommand AddCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
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
        #endregion
        public AddFoodItemDBViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            ItemToAdd = new LocalFoodItem();
            ItemToAdd.PropertyChanged += OnItemToAddPropertyChanged;
            AddCommand = new Command<LocalFoodItem>(execute: Add, canExecute: ValidateAddButton);
            CloseCommand = new Command(execute: Close);
            DBLocalAccess = new DataAccessLayer<LocalFoodItem>(GlobalSQLiteConnection.LocalDatabase);
            dialogService = new DialogService();


        }
        #region Methods
        private async void Close()
        {
            await navigationService.PopModal();
        }
        private async void Add(LocalFoodItem Parameter)
        {

            //TODO VALIDATIONS!
            if (FoodItemValidator.Check(Parameter))
                try
                {
                    await DBLocalAccess.Insert(Parameter);
                    //if program stays here the insert date will be incorrect
                    ItemToAdd.ResetValues();
                    ProgressIndicator = "Item added successfully!";
                    await SwitchProgressIndicator();
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
        }
        private bool ValidateAddButton(LocalFoodItem Parameter)
        {
            return FoodItemValidator.Check(Parameter);
        }
        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }
        private void OnItemToAddPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("I cahnged the can execute for add ");
            (AddCommand as Command).ChangeCanExecute();
        }
        #endregion

    }
}
