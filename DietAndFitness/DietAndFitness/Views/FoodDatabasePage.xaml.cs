using DietAndFitness.Models;
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
        public FoodDatabaseViewModel<LocalFoodItem> FoodDatabase { get; set; }
        public FoodDatabasePage ()
		{
            InitializeComponent();
            FoodDatabase = new FoodDatabaseViewModel<LocalFoodItem>(App.NavigationService);
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
            AddFoodItemDB.BindingContext = new AddFoodItemDB();
            await Navigation.PushAsync(AddFoodItemDB);
        }
        public async void EditFoodItemButton_Clicked(object sender, EventArgs e)
        {
            var EditFoodItemDB = new EditFoodItemDB();
            EditFoodItemDB.BindingContext = FoodDatabase;
            await Navigation.PushAsync(EditFoodItemDB);

        }
    }
}