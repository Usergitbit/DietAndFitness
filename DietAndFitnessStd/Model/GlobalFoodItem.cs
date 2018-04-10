using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DietAndFitness.Model
{
    [Table("GlobalFoodItem")]
    public class GlobalFoodItem : DatabaseEntity
    {

        private string calories;
        public string Calories
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
        private string carbohydrates;
        public string Carbohydrates
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
        private string proteins;
        public string Proteins
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
        private string fats;
        public string Fats
        {
            get
            {
                return fats;
            }
            set
            {
                if (value.Equals(fats))
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
                if (value.Equals(brand))
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
                if (value.Equals(cooking_mode))
                    return;
                cooking_mode = value;
                OnPropertyChanged();
            }
        }
        
        public GlobalFoodItem()
        {

        }
        public GlobalFoodItem(string _name)
        {
            Name = _name;
        }
    }
}
