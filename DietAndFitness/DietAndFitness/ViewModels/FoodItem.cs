using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness
{
    class FoodItem
    {
        private string name;
        private string proteins;
        private string carbohydrates;
        private string fats;
        private string quantity;
        private string calories;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }

        }
        public string Calories
        {
            get
            {
                return calories + " Calories";
            }

            set
            {
                calories = value;
            }

        }
        public string Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }

        }
        public string Proteins

        {
            get
            {
                return  proteins;
            }

            set
            {
                proteins = value;
            }

        }
        public string Carbohydrates
        {
            get
            {
                return carbohydrates;
            }

            set
            {
                carbohydrates = value;
            }

        }
        public string Fats
        {
            get
            {
                return fats;
            }

            set
            {
                fats = value;
            }

        }
        public string NutritionValues
        {
            get
            {
                return Carbohydrates + Proteins + Fats;
            }
        }
        public FoodItem(string _name, string _proteins, string _carbohydrates, string _fats, string _quantity, string _calories)
        {
            name = _name;
            proteins = _proteins;
            carbohydrates = _carbohydrates;
            fats = _fats;
            quantity = _quantity;
            calories = _calories;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
