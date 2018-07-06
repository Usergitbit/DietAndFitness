using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Class for an entry in the GlobalFoodItem table
/// </summary>
namespace DietAndFitness.Models
{
    /// <summary>
    /// Model class for food items from the table that comes with the app
    /// </summary>
    [Table("GlobalFoodItem")]
    public class GlobalFoodItem : DatabaseEntity
    {
        public byte[] GUID { get; set; }
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
                if(brand == null || value.Equals(brand))
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
                if (cooking_mode == null || value.Equals(cooking_mode))
                    return;
                cooking_mode = value;
                OnPropertyChanged();
            }
        }

        public GlobalFoodItem() : base()
        {
            Brand = String.Empty;
            CookingMode = String.Empty;
            Calories = 0;
            Carbohydrates = 0;
            Proteins = 0;
            Fats = 0;
        }
        public GlobalFoodItem(string _name, double? _proteins, double? _calories, double? _carbs, double? _fats, string _brand, string _cookingmode ) : base()
        {
            Name = _name;
            Calories = _calories;
            Carbohydrates = _carbs;
            Fats = _fats;
            Brand = _brand;
            CookingMode = _cookingmode;
            Proteins = _proteins;
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
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
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
