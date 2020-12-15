using DietAndFitness.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaloriesStatisticsPage : ContentPage
    {
        public CaloriesStatisticsViewModel StatisticsViewModel { get; set; }

        public CaloriesStatisticsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                StatisticsViewModel = (BindingContext as CaloriesStatisticsViewModel);
                await StatisticsViewModel.LoadData();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
              "Error",
              ex.Message,
              "Ok");
            }
            
        }

        private async void OnStartDateChanged(object sender, DateChangedEventArgs e)
        {
            if (StatisticsViewModel != null)
                await StatisticsViewModel?.LoadData();
        }

        private async void OnEndDateChanged(object sender, DateChangedEventArgs e)
        {
            if (StatisticsViewModel != null)
                await StatisticsViewModel?.LoadData();
        }
    }
}