using DietAndFitness.ViewModels;
using Syncfusion.SfSchedule.XForms;
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
	public partial class CalendarPage : ContentPage
	{
        CalendarViewModel CalendarViewModel { get; set; }
		public CalendarPage ()
		{
			InitializeComponent ();
            CalendarViewModel = new CalendarViewModel();
            BindingContext = CalendarViewModel;
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void SfSchedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            //WORKAROUDN UNTIL BUG IS FIXED;
            if(Device.RuntimePlatform == Device.UWP)
                ((sender as SfSchedule).BindingContext as CalendarViewModel).SelectedDate = e.Datetime;
        }
    }
}