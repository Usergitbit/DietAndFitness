using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodListPage : ContentPage
	{
		public FoodListPage ()
		{
			InitializeComponent ();
		}

        private void FoodItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null)
            {
                EditFoodItemButton.IsVisible = false;
                DeleteFoodItemButton.IsVisible = false;
            }
            else
            {
                EditFoodItemButton.IsVisible = true;
                DeleteFoodItemButton.IsVisible = true;
            }
        }
    }
}