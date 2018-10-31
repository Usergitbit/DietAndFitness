using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Entities;
using DietAndFitness.Services;
using DietAndFitness.Validators;
using DietAndFitness.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the AddFoodItemDB page
    /// </summary>
    public class ChangeFoodItemDBViewModel: ChangeBaseViewModel<LocalFoodItem>
    {
        public ChangeFoodItemDBViewModel() : base()
        {
        }
        public ChangeFoodItemDBViewModel(LocalFoodItem selectedItem) : base(selectedItem)
        {
        }

    }
}
