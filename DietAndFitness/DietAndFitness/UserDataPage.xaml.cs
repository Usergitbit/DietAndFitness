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
            await DisplayAlert("Choosing a formula", "This field controls which formula for calculating your needed calories will be used.\n" +
                                                     "The Mifflin-St Jeor Formula is the simplest and most accurate formula according to the American Dietetic Association.\n" +
                                                     "Men:\n 10 x Weight (kg) + 6.25 x Height (cm) - 5 x Age (y) + 5\n" +
                                                     "Women:\n 10 x Weight (kg) + 6.25 x Height (cm) - 5 x Age (y) -161\n" +
                                                     "The Katch-McArdle Formula is a variation of the Mifflin-St Jeor that will figure in your body fat for extra accuracy.\n" +
                                                     "The Harris-Benedict Formula was created in 1919 but it tends to overstate caloirc needs by 5% and tends to skew results towards obese and young people", "OK");
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

    }
}