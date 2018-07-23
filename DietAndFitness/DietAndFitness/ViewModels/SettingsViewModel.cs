using DietAndFitness.Core;
using DietAndFitness.Models;
using DietAndFitness.ViewModels.Secondary;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public static double? MaximumCalories
        {
            get
            {
                return new Sum(ActiveProfile).Calories;
            }
        }
        public static Profile ActiveProfile { get; set; }
        
    }
}
