using DietAndFitness.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using DietAndFitness.Controls;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using DietAndFitness.Validators;
using System.Timers;
using System.Threading.Tasks;
using DietAndFitness.Services;
using System.Runtime.CompilerServices;
using DietAndFitness.Views;
using DietAndFitness.ViewModels.Base;
using DietAndFitness.Core.Models;

namespace DietAndFitness.ViewModels
{
    /// <summary>
    /// ViewModel for the Food Database page
    /// </summary>
    public class FoodDatabaseViewModel : ListBaseViewModel<LocalFoodItem, ChangeFoodItemDB>
    {
        #region Members
        private string progressindicator = "Waiting for input...";
        #endregion
        #region Properties
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


        #endregion
        public FoodDatabaseViewModel() : base()
        {

        }
        #region Methods
        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }
        #endregion
    }
}
