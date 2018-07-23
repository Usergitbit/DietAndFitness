using DietAndFitness.Core;
using DietAndFitness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.ViewModels.Secondary
{
    public class Sum : ModelBase
    {
        private double? calories;
        private double? proteins;
        private double? carbohydrates;
        private double? fats;
        #region Properties
        public double? Calories
        {
            get
            {
                return calories;
            }
            set
            {
                if (calories == value)
                    return;
                calories = value;
                OnPropertyChanged();
            }
        }
        public double? Proteins
        {
            get
            {
                return proteins;
            }
            set
            {
                if (proteins == value)
                    return;
                proteins = value;
                OnPropertyChanged();
            }
        }

        public double? Carbohydrates
        {
            get
            {
                return carbohydrates;
            }
            set
            {
                if (carbohydrates == value)
                    return;
                carbohydrates = value;
                OnPropertyChanged();
            }
        }
        public double? Fats
        {
            get
            {
                return fats;
            }
            set
            {
                if (fats == value)
                    return;
                fats = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public Sum()
        {
            Calories = 0;
            Proteins = 0;
            Carbohydrates = 0;
            Fats = 0;
        }

        public Sum(Profile activeProfile)
        {
            Calories = activeProfile.Height + activeProfile.Weight * activeProfile.ActivityLevel;
        }

        public void Add(DailyFoodItem item)
        {
            Calories += item.Calories;
            Proteins += item.Proteins;
            Carbohydrates += item.Carbohydrates;
            Fats += item.Fats;
        }
        public void Reset()
        {
            Calories = 0;
            Proteins = 0;
            Carbohydrates = 0;
            Fats = 0;
        }
    }

}
