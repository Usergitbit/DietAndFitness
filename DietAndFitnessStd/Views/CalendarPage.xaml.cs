using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DietAndFitness.Views;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{
		public CalendarPage ()
		{
			InitializeComponent ();
		}

        private void OnViewButtonClicked(object sender, EventArgs e)
        {
           //TODO Reimplement as navigation page for detail page which switches content? Current Method recalls the whole MasterPage creation
            App.Current.MainPage = new MainPage { Detail = new NavigationPage(new FoodListPage()) };
            
        }
    }
}