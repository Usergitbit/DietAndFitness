using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Core.Models
{
    /// <summary>
    /// Model class for meals eaten during the day
    /// </summary>
    public class DailyFoodItem : DatabaseEntity
    {
        private LocalFoodItem foodItem;
        public LocalFoodItem FoodItem
        {
            get
            {
                return foodItem;
            }
            set
            {
                if (value == foodItem)
                    return;
                foodItem = value;
                OnPropertyChanged();
            }
        }
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
        public DailyFoodItem(DateTime date) : base(date)
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
            Name = string.Empty;
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
            Deleted = false;
            FoodItemID = null;
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
