using DietAndFitness.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyFoodListPage : ContentPage
    {
        public DailyFoodListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                await (BindingContext as DailyFoodListViewModel).LoadList();
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
              "Error",
              ex.Message,
              "Ok");
            }
        }
    }
}