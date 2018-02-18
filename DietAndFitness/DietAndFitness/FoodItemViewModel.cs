using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness
{
    class FoodItemViewModel
    {
        private string name;
        private string proteins;
        private string carbohydrates;
        private string fats;
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
        public string Proteins

        {
            get
            {
                return "Proteins " + proteins + " ";
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
                return "Carbohydrates " + carbohydrates + " ";
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
                return "Fats " + fats + " ";
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
        public FoodItemViewModel(string _name, string _proteins, string _carbohydrates, string _fats)
        {
            name = _name;
            proteins = _proteins;
            carbohydrates = _carbohydrates;
            fats = _fats;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
