using DietAndFitness.Entities;
using System;

namespace DietAndFitness.Validators
{
    /// <summary>
    /// Static class for validating a GlobalFoodItem
    /// </summary>
    public static class FoodItemValidator
    {
        public static bool Check(GlobalFoodItem Parameter)
        {
            if (Parameter == null)
                return false;
            if (Parameter.Name.Equals(String.Empty) || Parameter.Calories.ToString().Equals(String.Empty) || Parameter.Carbohydrates.ToString().Equals(String.Empty) || Parameter.Proteins.ToString().Equals(String.Empty) || Parameter.Fats.ToString().Equals(String.Empty))
                return false;
            return true;
        }
        public static bool Check(LocalFoodItem Parameter)
        {
            if (Parameter == null)
                return false;
            if (Parameter.Name.Equals(String.Empty) || Parameter.Calories.ToString().Equals(String.Empty) || Parameter.Carbohydrates.ToString().Equals(String.Empty) || Parameter.Proteins.ToString().Equals(String.Empty) || Parameter.Fats.ToString().Equals(String.Empty))
                return false;
            return true;
        }

    }
}
