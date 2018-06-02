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
    public class AddFoodItemDBViewModel<T> : ViewModelBase where T : DatabaseEntity, new()
    {
        #region Members
        private T itemToAdd;
        protected readonly IDialogService dialogService;
        private string progressindicator = "Waiting for input...";
        private NavigationService navigationService;
        protected DataAccessLayer<T> DBLocalAccess;
        #endregion
        #region Properties
        public T ItemToAdd
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
            ItemToAdd = new T();
            ItemToAdd.PropertyChanged += OnItemToAddPropertyChanged;
            AddCommand = new Command<T>(execute: Add, canExecute: ValidateAddButton);
            CloseCommand = new Command(execute: Close);
            DBLocalAccess = new DataAccessLayer<T>(GlobalSQLiteConnection.LocalDatabase);
            dialogService = new DialogService();


        }
        #region Methods
        private async void Close()
        {
            await navigationService.PopModal();
        }
        protected virtual async void Add(T Parameter)
        {
            
            if (Parameter.Check())
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
        protected virtual bool ValidateAddButton(T Parameter)
        {
            if(Parameter != null)
                return Parameter.Check();
            return false;
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
