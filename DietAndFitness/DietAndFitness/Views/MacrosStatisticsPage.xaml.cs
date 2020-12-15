using DietAndFitness.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MacrosStatisticsPage : ContentPage
	{
        public MacrosStatisticsViewModel MacrosStatisticsViewModel { get; set; }

        public MacrosStatisticsPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                MacrosStatisticsViewModel = (BindingContext as MacrosStatisticsViewModel);
                await MacrosStatisticsViewModel.LoadData();
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "Ok");
            }
        }
        private async void OnStartDateChanged(object sender, DateChangedEventArgs e)
        {
            if (MacrosStatisticsViewModel != null)
                await MacrosStatisticsViewModel?.LoadData();
        }

        private async void OnEndDateChanged(object sender, DateChangedEventArgs e)
        {
            if (MacrosStatisticsViewModel != null)
                await MacrosStatisticsViewModel?.LoadData();
        }
    }
}