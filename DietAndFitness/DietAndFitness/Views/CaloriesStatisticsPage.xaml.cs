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
	public partial class CaloriesStatisticsPage : ContentPage
	{
        public CaloriesStatisticsViewModel StatisticsViewModel { get; set; }

        public CaloriesStatisticsPage ()
		{
			InitializeComponent ();
            StatisticsViewModel = new CaloriesStatisticsViewModel();
            BindingContext = StatisticsViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StatisticsViewModel.LoadData();
        }

        private void OnStartDateChanged(object sender, DateChangedEventArgs e)
        {
            StatisticsViewModel.LoadData();
        }

        private void OnEndDateChanged(object sender, DateChangedEventArgs e)
        {
            StatisticsViewModel.LoadData();
        }
    }
}