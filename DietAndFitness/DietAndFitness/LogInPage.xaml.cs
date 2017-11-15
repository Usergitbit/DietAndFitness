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
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        async void OnLogInClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDataPage());
        }
        async void OnSignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDataPage());
        }
    }
}