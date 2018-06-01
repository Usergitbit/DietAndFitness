using DietAndFitness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Validators
{
    /// <summary>
    /// Static class for validating a GlobalFoodItem
    /// </summary>
    public static class GlobalFoodItemValidator
    {
        public static bool Check(GlobalFoodItem Parameter)
        {
            if (Parameter == null)
                return false;
            if (Parameter.Name.Equals(String.Empty) || Parameter.Calories.Equals(String.Empty) || Parameter.Carbohydrates.Equals(String.Empty) || Parameter.Proteins.Equals(String.Empty) || Parameter.Fats.Equals(String.Empty))
                return false;
            return true;
        }
    }
}
