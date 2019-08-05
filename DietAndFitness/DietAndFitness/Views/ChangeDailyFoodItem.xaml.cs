using DietAndFitness.Extensions;
using Syncfusion.SfAutoComplete.XForms;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangeDailyFoodItem : ContentPage
    {
        DebounceDispatcher dispatcher = new DebounceDispatcher();
        public ChangeDailyFoodItem()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            autoCompleteSearchBar.Unfocus();
            entryQuantity.Focus();
            myLlistView.ScrollTo(e.SelectedItem, ScrollToPosition.MakeVisible, true);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as DietAndFitness.ViewModels.ChangeDailyFoodItemViewModel;
            await vm.LoadList();
            if (myLlistView.SelectedItem == null)
                autoCompleteSearchBar.Focus();
            else
                entryQuantity.Focus();
            //var x = lol;
            //lol.AutoCompleteSource = vm.FoodItems;
        }

        public class SmartAutoComplete : SfAutoComplete
        {
            void wtf()
            {

            }
        }

        private void AutoCompleteSearchBar_FilterCollectionChanged(object sender, FilterCollectionChangedEventArgs e)
        {
            dispatcher.Debounce(500, () =>
             {
                 Device.BeginInvokeOnMainThread(() =>
                 {
                     myLlistView.ItemsSource = autoCompleteSearchBar.FilteredItems;
                     Debug.WriteLine("Executed from Device.BeginInvokeOnMainThread on filter complete");
                 });
             });
        }
    }
}