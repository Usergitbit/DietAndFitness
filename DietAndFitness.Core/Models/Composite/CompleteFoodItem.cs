using System;

namespace DietAndFitness.Core.Models.Composite
{
    public class CompleteFoodItem : DatabaseEntity
    {
        private DailyFoodItem dailyFoodItem;
        private LocalFoodItem localFoodItem;
        public double? Quantity { get; set; }
        public double Calories
        {
            get
            {
                return (DailyFoodItem.Quantity * LocalFoodItem.Calories.Value) / 100;
            }
        }
        public double Carbohydrates
        {
            get
            {
                return (DailyFoodItem.Quantity * LocalFoodItem.Carbohydrates.Value) / 100;
            }
        }
        public double Proteins
        {
            get
            {
                return (DailyFoodItem.Quantity * LocalFoodItem.Proteins.Value) / 100;
            }
        }
        public double Fats
        {
            get
            {
                return (DailyFoodItem.Quantity * LocalFoodItem.Fats.Value) / 100;
            }
        }
        public string RandomString { get; set; }
        public DailyFoodItem DailyFoodItem
        {
            get
            {
                if (dailyFoodItem == null)
                    return new DailyFoodItem();
                return dailyFoodItem;
            }
            set
            {
                if (dailyFoodItem == value)
                    return;
                dailyFoodItem = value;
                OnPropertyChanged();
            }
        }
        public LocalFoodItem LocalFoodItem
        {
            get
            {
                return localFoodItem;
            }
            set
            {
                if (localFoodItem == value)
                    return;
                localFoodItem = value;
                OnPropertyChanged();
            }
        }
        public CompleteFoodItem()
        {

        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override void ResetValues()
        {
            throw new NotImplementedException();
        }
    }
}
