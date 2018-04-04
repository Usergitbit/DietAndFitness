using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness
{
    public class FoodListViewModel
    {
        public static ObservableCollection<FoodItem> FoodList;


        public FoodListViewModel()
        {
            //FoodList = new ObservableCollection<FoodItem>();
            //FoodList.Add(new FoodItem("Chocolate", "5", "40", "20", "100", "400"));
            //FoodList.Add(new FoodItem("Candy", "5", "40", "20", "100", "400"));
            //for (int i = 0; i < 20; i++)
            //    FoodList.Add(new FoodItem("Pretzels", "5", "40", "20", "100", "400"));
            //FoodList.Add(new FoodItem("Chocolate", "5", "40", "20", "100", "400"));
            //FoodList.Add(new FoodItem("Candy", "5", "40", "20", "100", "400"));
        }

        public void AddItem(FoodItem Item)
        {
            FoodList.Add(Item);
        }

        public async void LoadList (Task<List<FoodItem>> foodItems)
        {
            FoodList = new ObservableCollection<FoodItem>(await foodItems);
        }
    }
}
