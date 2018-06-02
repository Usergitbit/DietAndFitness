using DietAndFitness.Models;
using DietAndFitness.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class EditDailyFoodItemViewModel : EditFoodItemDBViewModel<DailyFoodItem>
    {
        public EditDailyFoodItemViewModel(DailyFoodItem selectedItem, NavigationService navigationService) : base(selectedItem,navigationService)
        {

        }
    }
}
