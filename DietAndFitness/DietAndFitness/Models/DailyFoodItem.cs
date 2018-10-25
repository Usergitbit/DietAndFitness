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
        int? foodItemID;
        public int? FoodItemID
        {
            get
            {
                return foodItemID;
            }
            set
            {
                if (foodItemID == value)
                    return;
                foodItemID = value;
                OnPropertyChanged();
            }
        }
        private int? profileID;
        public int? ProfileID
        {
            get
            {
                return profileID;
            }
            set
            {
                if (profileID == value)
                    return;
                profileID = value;
                OnPropertyChanged();
            }
        }
        public DailyFoodItem()
        {

        }

        public override bool IsValid()
        {
            if (Name.Equals(String.Empty) || Quantity.ToString().Equals(String.Empty) || FoodItemID.ToString().Equals(String.Empty))
                return false;
            return true;
        }

        public override void ResetValues()
        {
            Quantity = 0;
            Name = String.Empty;
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
            Deleted = false;
            FoodItemID = 0;
        }

        public void SetValues(DatabaseEntity selectedItem)
        {
            if(selectedItem.GetType() == typeof(GlobalFoodItem))
            {
                GlobalFoodItem convertedSelectedItem = (GlobalFoodItem)selectedItem;
                Name = convertedSelectedItem.Name;
                FoodItemID = convertedSelectedItem.ID;
            }
            else
            {
                LocalFoodItem convertedSelectedItem = (LocalFoodItem)selectedItem;
                Name = convertedSelectedItem.Name;
                FoodItemID = convertedSelectedItem.ID;
            }
        }
    }
}
