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
	public partial class MacrosStatisticsPage : ContentPage
	{
        public MacrosStatisticsViewModel MacrosStatisticsViewModel { get; set; }

        public MacrosStatisticsPage ()
		{
			InitializeComponent ();
            MacrosStatisticsViewModel = new MacrosStatisticsViewModel();
            BindingContext = MacrosStatisticsViewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MacrosStatisticsViewModel.LoadData();
        }
        private void OnStartDateChanged(object sender, DateChangedEventArgs e)
        {
            MacrosStatisticsViewModel.LoadData();
        }

        private void OnEndDateChanged(object sender, DateChangedEventArgs e)
        {
            MacrosStatisticsViewModel.LoadData();
        }
    }
}