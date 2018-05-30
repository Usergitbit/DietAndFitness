using DietAndFitness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodDatabasePage : ContentPage
	{
        public FoodDatabaseViewModel FoodDatabase { get; set; }
        public FoodDatabasePage ()
		{
            InitializeComponent();
            FoodDatabase = new FoodDatabaseViewModel();
            BindingContext = FoodDatabase;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FoodDatabase.LoadList();
        }
        public async void AddFoodItemButton_Clicked(object sender, EventArgs e)
        {
            var AddFoodItemDB = new AddFoodItemDB();
            AddFoodItemDB.BindingContext = FoodDatabase;
            await Navigation.PushAsync(AddFoodItemDB);
        }
        public async void EditFoodItemButton_Clicked(object sender, EventArgs e)
        {
            var EditFoodItemDB = new EditFoodItemDB();
            EditFoodItemDB.BindingContext = FoodDatabase;
            FoodDatabase.BindSelectedItem();
            await Navigation.PushAsync(EditFoodItemDB);

        }
    }
}