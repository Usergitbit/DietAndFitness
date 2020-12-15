using DietAndFitness.ViewModels;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;

namespace DietAndFitness.Services
{
    public static class ViewModelLocator
    {
        public static Dictionary<Type, Type> ViewVM { get; set; }
        static ViewModelLocator()
        {
            ViewVM = new Dictionary<Type, Type>();
            ViewVM.Add(typeof(ChangeFoodItemDB), typeof(ChangeFoodItemDBViewModel));
            ViewVM.Add(typeof(ChangeDailyFoodItem), typeof(ChangeDailyFoodItemViewModel));
            ViewVM.Add(typeof(DailyFoodListPage), typeof(DailyFoodListViewModel));
        }
    }
}
