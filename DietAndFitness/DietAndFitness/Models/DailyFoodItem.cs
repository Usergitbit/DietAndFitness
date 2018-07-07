using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Models
{
    /// <summary>
    /// Model class for meals eaten during the day
    /// </summary>
    [Table("DailyFoodItem")]
    public class DailyFoodItem : DatabaseEntity
    {

         
        private double quantity;
        public double Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (value == quantity)
                    return;
                quantity = value;
                OnPropertyChanged();
            }
        }
        private double? calories;
        public double? Calories
        {
            get
            {
                return calories;
            }
            set
            {
                if (value.Equals(calories))
                    return;
                calories = value;
                OnPropertyChanged();
            }
        }
        private double? carbohydrates;
        public double? Carbohydrates
        {
            get
            {
                return carbohydrates;
            }

            set
            {
                if (value.Equals(carbohydrates))
                    return;
                carbohydrates = value;
                OnPropertyChanged();
            }
        }
        private double? proteins;
        public double? Proteins
        {
            get
            {
                return proteins;
            }
            set
            {
                if (value.Equals(proteins))
                    return;
                proteins = value;
                OnPropertyChanged();
            }
        }
        private double? fats;
        int? foodItemPK;
        public double? Fats
        {
            get
            {
                return fats;
            }
            set
            {
                if (value == fats)
                    return;
                fats = value;
                OnPropertyChanged();
            }
        }
        public int? FoodItemPK
        {
            get
            {
                return foodItemPK;
            }
            set
            {
                if (foodItemPK == value)
                    return;
                foodItemPK = value;
                OnPropertyChanged();
            }
        }
        public DailyFoodItem()
        {

        }

        public override bool IsValid()
        {
            if (Name.Equals(String.Empty) || Calories.ToString().Equals(String.Empty) || Carbohydrates.ToString().Equals(String.Empty) || Proteins.ToString().Equals(String.Empty) || Fats.ToString().Equals(String.Empty) || Quantity.ToString().Equals(String.Empty) || FoodItemPK.ToString().Equals(String.Empty))
                return false;
            return true;
        }

        public override void ResetValues()
        {
            Quantity = 0;
            Name = String.Empty;
            Calories = 0;
            Carbohydrates = 0;
            Proteins = 0;
            Fats = 0;
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
            Deleted = false;
            FoodItemPK = 0;
        }

        public void SetValues(DatabaseEntity selectedItem)
        {
            if(selectedItem.GetType() == typeof(GlobalFoodItem))
            {
                GlobalFoodItem convertedSelectedItem = (GlobalFoodItem)selectedItem;
                Name = convertedSelectedItem.Name;
                FoodItemPK = convertedSelectedItem.ID;
                Calories = (Quantity * convertedSelectedItem.Calories) / 100;
                Proteins = (Quantity * convertedSelectedItem.Proteins) / 100;
                Carbohydrates = (Quantity * convertedSelectedItem.Carbohydrates) / 100;
                Fats = (Quantity * convertedSelectedItem.Fats) / 100;
            }
            else
            {
                LocalFoodItem convertedSelectedItem = (LocalFoodItem)selectedItem;
                Name = convertedSelectedItem.Name;
                FoodItemPK = convertedSelectedItem.ID;
                Calories = (Quantity * convertedSelectedItem.Calories) / 100;
                Proteins = (Quantity * convertedSelectedItem.Proteins) / 100;
                Carbohydrates = (Quantity * convertedSelectedItem.Carbohydrates) / 100;
                Fats = (Quantity * convertedSelectedItem.Fats) / 100;
            }

        }
    }
}
