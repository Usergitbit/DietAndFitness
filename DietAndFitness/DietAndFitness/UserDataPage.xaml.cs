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
    public partial class UserDataPage : ContentPage
    {
        public UserDataPage()
        {
            InitializeComponent();
        }
        async void OnFormulaHelpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormulaHelpPage());
           
        }
         void OnActivityHelpClicked(object sender, EventArgs e)
        {
            DisplayAlert("Activity Level", "Info about activity levels", "OK");
        }
    void OnFormulaKMSelected(object sender, EventArgs e)
        {
            if (FormulaPicker.SelectedIndex == 1)
            {
                BodyFatLabel.IsVisible = true;
                BodyFatTextBox.IsVisible = true;
                BodyFatPercentageLabel.IsVisible = true;
            }
            else
            {
                BodyFatLabel.IsVisible = false;
                BodyFatTextBox.IsVisible = false;
                BodyFatPercentageLabel.IsVisible = false;
            }
        }
    async void OnCalculateClicked(object sender, EventArgs e)
    {

            TotalCaloriesLabel.Text = "Place holder text for calorie results";
            FoodListViewModel F = new FoodListViewModel();
            await Navigation.PushAsync(new FoodListPage());
    }


    }

   
}