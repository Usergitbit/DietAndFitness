using DietAndFitness.Core;
using DietAndFitness.Models;
using DietAndFitness.Services;
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
            var allFoodItemsDB = await DBLocalAccess.GetAll();
            FoodItems.Clear();
            CurrentValues.Reset();
            foreach (var item in allFoodItemsDB)
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
        public class Sum : ModelBase
        {
            private double? calories;
            private double? proteins;
            private double? carbohydrates;
            private double? fats;
            #region Properties
            public double? Calories
            {
                get
                {
                    return calories;
                }
                set
                {
                    if (calories == value)
                        return;
                    calories = value;
                    OnPropertyChanged();
                }
            }
            public double? Proteins
            {
                get
                {
                    return proteins;
                }
                set
                {
                    if (proteins == value)
                        return;
                    proteins = value;
                    OnPropertyChanged();
                }
            }

            public double? Carbohydrates
            {
                get
                {
                    return carbohydrates;
                }
                set
                {
                    if (carbohydrates == value)
                        return;
                    carbohydrates = value;
                    OnPropertyChanged();
                }
            }
            public double? Fats
            {
                get
                {
                    return fats;
                }
                set
                {
                    if (fats == value)
                        return;
                    fats = value;
                    OnPropertyChanged();
                }
            }
            #endregion
            public Sum()
            {
                Calories = 0;
                Proteins = 0;
                Carbohydrates = 0;
                Fats = 0;
            }
            public void Add(DailyFoodItem item)
            {
                Calories += item.Calories;
                Proteins += item.Proteins;
                Carbohydrates += item.Carbohydrates;
                Fats += item.Fats;
            }
            public void Reset()
            {
                Calories = 0;
                Proteins = 0;
                Carbohydrates = 0;
                Fats = 0;
            }
        }
    }
}
