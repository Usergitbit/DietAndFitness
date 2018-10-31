using DietAndFitness.Entities;
using DietAndFitness.Services;
using DietAndFitness.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class EditDailyFoodItemViewModel : ChangeBaseViewModel<DailyFoodItem>
    {
        public EditDailyFoodItemViewModel(DailyFoodItem selectedItem, NavigationService navigationService) : base(selectedItem,navigationService)
        {

        }
    }
}
