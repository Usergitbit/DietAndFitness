using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness
{
    class FoodListViewModel
    {
        public static List<FoodItemViewModel> FoodList;

        public FoodListViewModel()
        {
            FoodList = new List<FoodItemViewModel>();
            FoodList.Add(new FoodItemViewModel("Chocolate", "5", "40", "20", "100", "400"));
            FoodList.Add(new FoodItemViewModel("Candy", "5", "40", "20", "100", "400"));
            for (int i = 0; i < 20; i++)
                FoodList.Add(new FoodItemViewModel("Pretzels", "5", "40", "20", "100", "400"));
            FoodList.Add(new FoodItemViewModel("Chocolate", "5", "40", "20", "100", "400"));
            FoodList.Add(new FoodItemViewModel("Candy", "5", "40", "20", "100", "400"));
        }



    }
}
