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
    public class DailyFoodListViewModel : FoodDatabaseViewModel<DailyFoodItem>
    {
        public Sum CurrentValues { get; set; }
        public Sum MaximumValues { get; set; }
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
            var todayFoodItems = await DBLocalAccess.GetByDate<DailyFoodItem>(DateTime.Today);
            FoodItems.Clear();
            CurrentValues.Reset();
            foreach (var item in todayFoodItems)
            {
                FoodItems.Add(item);
                CurrentValues.Add(item);
            }
           

        }
        protected override async void OpenAddPageFunction()
        {
            var addDailyFoodItemPage = new AddDailyFoodItem();
            addDailyFoodItemPage.BindingContext = new AddDailyFoodItemViewModel(navigationService);
            await navigationService.PushModal(addDailyFoodItemPage);
            SelectedItem = null;
        }

        protected override async void OpenEditPageFunction(DailyFoodItem parameter)
        {
            var editDailyFoodItemPage = new EditDailyFoodItem();
            editDailyFoodItemPage.BindingContext = new EditDailyFoodItemViewModel(parameter, navigationService);
            await navigationService.PushModal(editDailyFoodItemPage);
            SelectedItem = null;
        }
    }
}
