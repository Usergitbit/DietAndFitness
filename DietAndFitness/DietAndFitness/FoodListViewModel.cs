using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness
{
    class FoodListViewModel
    {
        public static List<FoodItemViewModel> FoodList;
        public FoodListViewModel()
        {
            FoodList = new List<FoodItemViewModel>();
            FoodList.Add(new FoodItemViewModel("Ciocolata", "5", "40", "20"));
            FoodList.Add(new FoodItemViewModel("Bomboane", "5", "40", "20"));
            FoodList.Add(new FoodItemViewModel("Covrigi", "5", "40", "20"));
        }
    }
}
