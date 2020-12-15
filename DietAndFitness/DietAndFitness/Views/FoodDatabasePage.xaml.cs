using DietAndFitness.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodDatabasePage : ContentPage
    {
        public FoodDatabasePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await (BindingContext as FoodDatabaseViewModel).LoadList();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "Ok");
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}