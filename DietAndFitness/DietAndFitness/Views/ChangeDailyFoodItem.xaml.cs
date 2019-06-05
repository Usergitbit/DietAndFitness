using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeDailyFoodItem : ContentPage
    {
        public ChangeDailyFoodItem()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            searchBar.Unfocus();
            entryQuantity.Focus();
            myLlistView.ScrollTo(e.SelectedItem, ScrollToPosition.Start, true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (myLlistView.SelectedItem == null)
                searchBar.Focus();
            else
                entryQuantity.Focus();
        }


    }
}