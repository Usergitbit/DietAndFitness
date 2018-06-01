﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Class for an entry in the GlobalFoodItem table
/// </summary>
namespace DietAndFitness.Models
{
    /// <summary>
    /// Model class for food items from the table that the user introduces
    /// </summary>
    [Table("LocalFoodItem")]
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
                if (brand == null || value.Equals(brand))
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

        public void ResetValues()
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
    }
}
