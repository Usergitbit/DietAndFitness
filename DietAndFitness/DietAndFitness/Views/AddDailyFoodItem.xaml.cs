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
	public partial class AddDailyFoodItem : ContentPage
	{
		public AddDailyFoodItem ()
		{
			InitializeComponent ();
		}

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //doesn't work on UWP
            searchBar.Unfocus();
            entryQuantity.Focus();
            myLlistView.ScrollTo(e.SelectedItem,ScrollToPosition.Start,true);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            searchBar.Focus();
        }
    }
}