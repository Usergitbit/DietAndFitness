using DietAndFitness.Core.Models;
using DietAndFitness.Interfaces;
using DietAndFitness.ViewModels.Base;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the AddFoodItemDB page
    /// </summary>
    public class ChangeFoodItemDBViewModel: ChangeBaseViewModel<LocalFoodItem>
    {
        private string progressindicator = "Waiting for input...";
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

        public ChangeFoodItemDBViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
        }
        public override async Task InitializeAsync(params object[] parameters)
        {
            var currentItemId = (int?)parameters.ElementAtOrDefault(0);
            if (currentItemId != null)
            {
                CurrentItem = await DBLocalAccess.LocalFoodItems.GetAsync(dfi => dfi.ID == currentItemId);
            }
        }

        protected override void OnOperationSuccess()
        {
            ProgressIndicator = "Operation succeeded!";
        }

        protected override void OnOperationFailiure()
        {
            ProgressIndicator = "Operation failed!";
        }

        protected override async Task Operation(LocalFoodItem parameter)
        {
            if (parameter.ID == null)
            {

                if (parameter.IsValid())
                    try
                    {
                        await DBLocalAccess.LocalFoodItems.InsertAsync(parameter);
                        //if program stays here the insert date will be incorrect
                        // we lose the reference when we make a new item so we have to unsubscribe here to prevent memory leaks
                        CurrentItem.PropertyChanged -= OnCurrentItemPropertyChanged;
                        CurrentItem = new LocalFoodItem();
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
                        await DBLocalAccess.LocalFoodItems.UpdateAsync(parameter);
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
    }
}
