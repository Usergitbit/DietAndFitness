using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Models;
using DietAndFitness.Services;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the EditFoodItemDB page
    /// </summary>
    class EditFoodItemDBViewModel : ViewModelBase
    {
        #region Members
        private GlobalFoodItem itemToEdit;
        private readonly IDialogService dialogService;
        private string progressindicator = "Waiting for input...";
        private NavigationService navigationService;
        private DataAccessLayer<GlobalFoodItem> DBAccess;
        #endregion
        #region Properties
        public GlobalFoodItem ItemToEdit
        {
            get
            {
                return itemToEdit;
            }

            set
            {
                if (value == itemToEdit)
                    return;
                itemToEdit = value;
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

        public ICommand CloseCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }
        #endregion
        public EditFoodItemDBViewModel(GlobalFoodItem selectedItem, NavigationService navigationService)
        {
            dialogService = new DialogService();
            itemToEdit = selectedItem;
            this.navigationService = navigationService;
            DBAccess = new DataAccessLayer<GlobalFoodItem>(GlobalSQLiteConnection.Database);
            CloseCommand = new Command(execute: Close);
            ConfirmCommand = new Command<GlobalFoodItem>(execute: Edit, canExecute: ValidateConfirmButton);
            ItemToEdit.PropertyChanged += OnItemToEditPropertyChanged;
        }
        #region Methods
        private void OnItemToEditPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(ItemToEdit != null)
                (ConfirmCommand as Command).ChangeCanExecute();
        }

        private async void Close()
        {
            await navigationService.PopModal();
        }

        private async void Edit(GlobalFoodItem parameter)
        {
            if (parameter != null)
            {
                try
                {
                    await DBAccess.Update(parameter);
                    parameter.Clean();
                    Debug.WriteLine(await DBAccess.Update(parameter));
                    ProgressIndicator = "Item edited successfully!";
                    OnItemToEditPropertyChanged(null, null);
                }
                catch(Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
            }
            else
                Debug.WriteLine("parameter was null");
        }
        private bool ValidateConfirmButton(GlobalFoodItem parameter)
        {
            if (parameter == null)
                Debug.WriteLine("parameter EDIT was null");
            else
            {
                Debug.WriteLine("I got here");
                if (parameter.IsDirty == true)
                {
                    Debug.WriteLine("Item is dirty");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Item is not dirty");
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}
