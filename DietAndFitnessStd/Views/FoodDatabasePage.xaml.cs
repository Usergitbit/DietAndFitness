using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DietAndFitness.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DietAndFitness.Model;
using System.Diagnostics;
namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodDatabasePage : ContentPage
	{
        public FoodItemsViewModel FoodDatabase { get; set; }
        public FoodDatabasePage ()
		{
           
            InitializeComponent ();
            FoodDatabase = new FoodItemsViewModel(SQLiteConnection.Database);
            BindingContext = FoodDatabase;
           
        }

        protected override void OnAppearing()
        {
            FoodDatabase.LoadList();
        }

        public void AddFoodItemButton_Clicked(object sender, EventArgs e)
        {
            FoodDatabase.Add();
        }
    }
}