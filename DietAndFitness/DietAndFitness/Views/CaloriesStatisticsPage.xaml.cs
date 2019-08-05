using DietAndFitness.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CaloriesStatisticsPage : ContentPage
	{
        public CaloriesStatisticsViewModel StatisticsViewModel { get; set; }

        public CaloriesStatisticsPage ()
		{
			InitializeComponent ();
            StatisticsViewModel = new CaloriesStatisticsViewModel();
            BindingContext = StatisticsViewModel;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await StatisticsViewModel.LoadData();
        }

        private async void OnStartDateChanged(object sender, DateChangedEventArgs e)
        {
            await StatisticsViewModel.LoadData();
        }

        private async void OnEndDateChanged(object sender, DateChangedEventArgs e)
        {
            await StatisticsViewModel.LoadData();
        }
    }
}