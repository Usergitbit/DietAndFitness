using DietAndFitness.Core;
using DietAndFitness.Models;
using DietAndFitness.Services;
using DietAndFitness.ViewModels.Secondary;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class DailyFoodListViewModel : FoodDatabaseViewModel<CompleteFoodItem>
    {
        private Sum currentValues;
        private Sum maximumValues;
        public Sum CurrentValues
        {
            get
            {
                return currentValues;
            }
            set
            {
                if (currentValues == value)
                    return;
                currentValues = value;
                OnPropertyChanged();
            }
        }
        public Sum MaximumValues
        {
            get
            {
                return maximumValues;
            }
            set
            {
                if (maximumValues == value)
                    return;
                maximumValues = value;
                OnPropertyChanged();
            }
        }
        public DailyFoodListViewModel(NavigationService navigationService) : base(navigationService)
        {
            CurrentValues = new Sum();
            MaximumValues = new Sum();
            CurrentValues.Calories = 1200.51;
            MaximumValues.Calories = 1400;
            MaximumValues.Fats = 100;
            MaximumValues.Carbohydrates = 100;
            MaximumValues.Proteins = 150;
            

        }

        public async override void LoadList()
        {
            var todayFoodItems = await DBLocalAccess.GetCompleteItemAsync();
            FoodItems.Clear();
            CurrentValues.Reset();
            foreach (var item in todayFoodItems)
            {
                FoodItems.Add(item);
                CurrentValues.Add(item);
            }
            var currentProfile = await DBLocalAccess.GetCurrentProfile();
            MaximumValues = currentProfile.GetMaximumValues();
            MaximumValues.Calories += 200;
        }
        protected override async void OpenAddPageFunction()
        {
            var addDailyFoodItemPage = new AddDailyFoodItem();
            addDailyFoodItemPage.BindingContext = new AddDailyFoodItemViewModel(navigationService);
            await navigationService.PushModal(addDailyFoodItemPage);
            SelectedItem = null;
        }

        protected override async void OpenEditPageFunction(CompleteFoodItem parameter)
        {
            var editDailyFoodItemPage = new EditDailyFoodItem();
            editDailyFoodItemPage.BindingContext = new EditDailyFoodItemViewModel(parameter.DailyFoodItem, navigationService);
            await navigationService.PushModal(editDailyFoodItemPage);
            SelectedItem = null;
        }

        public override RelayCommand ConfirmDeleteCommand
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

                                                   await DBLocalAccess.Delete((SelectedItem as CompleteFoodItem).DailyFoodItem);
                                                   LoadList();
                                                   SelectedItem = null;
                                               }
                                               catch (Exception ex)
                                               {
                                                   await dialogService.ShowError(ex, "Error", "Ok", null);
                                               }
                                           else
                                           {
                                               await dialogService.ShowMessage("Preset food items cannot be deleted", "Error");
                                           }
                                       }
                                   });
                           }, ValidateDeleteButton));
            }
        }
    }
}
