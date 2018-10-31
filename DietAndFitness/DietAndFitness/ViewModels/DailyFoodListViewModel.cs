using DietAndFitness.Core;
using DietAndFitness.Entities;
using DietAndFitness.Services;
using DietAndFitness.ViewModels.Base;
using DietAndFitness.ViewModels.Secondary;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class DailyFoodListViewModel : ListBaseViewModel<CompleteFoodItem, ChangeDailyFoodItem>
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
        public DailyFoodListViewModel() : base()
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
            Items.Clear();
            CurrentValues.Reset();
            foreach (var item in todayFoodItems)
            {
                Items.Add(item);
                CurrentValues.Add(item);
            }
            var currentProfile = await DBLocalAccess.GetCurrentProfile();
            MaximumValues = currentProfile.GetMaximumValues();
            MaximumValues.Calories += 200;
        }
        protected async override void ExecuteDelete(bool result)
        {
            if (result == true)
            {
                try
                {
                    await DBLocalAccess.Delete(SelectedItem.DailyFoodItem);
                    LoadList();
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
            };
        }
        //protected override async void OpenAddPageFunction()
        //{
        //    var addDailyFoodItemPage = new AddDailyFoodItem();
        //    addDailyFoodItemPage.BindingContext = new ChangeDailyFoodItemViewModel(navigationService);
        //    await navigationService.PushModal(addDailyFoodItemPage);
        //    SelectedItem = null;
        //}

        //protected override async void OpenEditPageFunction(CompleteFoodItem parameter)
        //{
        //    var editDailyFoodItemPage = new EditDailyFoodItem();
        //    editDailyFoodItemPage.BindingContext = new EditDailyFoodItemViewModel(parameter.DailyFoodItem, navigationService);
        //    await navigationService.PushModal(editDailyFoodItemPage);
        //    SelectedItem = null;
        //}


    }
}
