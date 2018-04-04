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
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
            //On UWP the menu is by default always ON, not disabling the following results in the title appearing two times on UWP
            //if (Device.RuntimePlatform.Equals(Device.UWP))
            //{
            //    NavigationPage.SetHasNavigationBar(this, false);
            //    NavigationPage.SetHasBackButton(this, false);
            //}
        }
    }
}