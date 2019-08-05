using System;
/// <summary>
/// Class for an entry in the GlobalFoodItem table
/// </summary>
namespace DietAndFitness.Core.Models
{
    /// <summary>
    /// Model class for food items from the table that the user introduces
    /// </summary>
    public class LocalFoodItem : DatabaseEntity
    {

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
        private string brand;
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                //if the new string is null trying to call Equals on it will throw an Exception
                if (value?.Equals(brand) ?? true)
                    return;
                brand = value;
                OnPropertyChanged();
            }
        }
        private string cooking_mode;
        public string CookingMode
        {
            get
            {
                return cooking_mode;
            }
            set
            {
                //if the new string is null trying to call Equals on it will throw an Exception
                if (value?.Equals(cooking_mode) ?? true)
                    return;
                cooking_mode = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Cast operator implementation
        /// </summary>
        /// <param name="v"></param>
        public static implicit operator LocalFoodItem(GlobalFoodItem v)
        {
            return new LocalFoodItem(v);
        }

        public LocalFoodItem() : base()
        {
            Brand = String.Empty;
            CookingMode = String.Empty;
            Calories = 0;
            Carbohydrates = 0;
            Proteins = 0;
            Fats = 0;
        }
        public LocalFoodItem(string _name, double? _proteins, double? _calories, double? _carbs, double? _fats, string _brand, string _cookingmode) : base()
        {
            Name = _name;
            Calories = _calories;
            Carbohydrates = _carbs;
            Fats = _fats;
            Brand = _brand;
            CookingMode = _cookingmode;
            Proteins = _proteins;
        }
        /// <summary>
        /// Copies each property from globalFoodItem into a new LocalFoodItem and sets the ID to null
        /// </summary>
        /// <param name="globalFoodItem"></param>
        public LocalFoodItem(GlobalFoodItem globalFoodItem)
        {
            foreach(var property in this.GetType().GetProperties())
            {
                property.SetValue(this, globalFoodItem.GetType().GetProperty(property.Name).GetValue(globalFoodItem));
            }
            //required otherwise insert will fail Primary Key constraint
            ID = null;
        }
        public override void ResetValues()
        {
            Name = String.Empty;
            Brand = String.Empty;
            CookingMode = String.Empty;
            Calories = 0;
            Carbohydrates = 0;
            Proteins = 0;
            Fats = 0;
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
            Deleted = false;
        }

        public override bool IsValid()
        {
            if (Name.Equals(String.Empty) || Calories.ToString().Equals(String.Empty) || Carbohydrates.ToString().Equals(String.Empty) || Proteins.ToString().Equals(String.Empty) || Fats.ToString().Equals(String.Empty))
                return false;
            return true;
        }
    }
}
