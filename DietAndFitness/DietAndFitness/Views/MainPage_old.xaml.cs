using DietAndFitness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DietAndFitness.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage_old : ContentPage
	{
       // public FoodDatabaseViewModel FoodDatabase { get; set; }
        public MainPage_old ()
		{
			InitializeComponent ();
            //FoodDatabase = new FoodDatabaseViewModel();
            //BindingContext = FoodDatabase;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
           // FoodDatabase.LoadList();
        }
        public void AddFoodItemButton_Clicked(object sender, EventArgs e)
        {
            //FoodDatabase.Add();
        }
    }
}