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

        async void OnBFHelpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
        }

         async void OnActivityLevelHelpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogInPage());
        }

    }
}