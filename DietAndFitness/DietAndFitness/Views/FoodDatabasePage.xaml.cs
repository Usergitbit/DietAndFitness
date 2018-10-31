using DietAndFitness.Entities;
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
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}