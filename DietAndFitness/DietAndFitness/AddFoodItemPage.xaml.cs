using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFoodItemPage : ContentPage
    {
        FoodItemViewModel content = null;
        public AddFoodItemPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            AddFoodList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue)) ;
            else
                AddFoodList.ItemsSource = FoodListViewModel.FoodList.Where(i => i.Name.Contains(e.NewTextValue));

            AddFoodList.EndRefresh();
        }


        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           if(content == e.Item as FoodItemViewModel)
            content = null;
           else;
            content = e.Item as FoodItemViewModel;


        }

        private async void AddFoodButton_Clicked(object sender, EventArgs e)
        {
            if (content == null)
              await  DisplayAlert("Error", "No item selected", "Ok");
            else
                if( String.IsNullOrWhiteSpace(QuantityTextBox.Text))
                    await DisplayAlert("Error", "No quantity entered", "Ok");
                else
                {
                     FoodListViewModel.FoodList.Add(new FoodItemViewModel("Kek", "14", "88", "20", "20", "14"));
                     await Navigation.PopAsync();
                }
        }
    }
}