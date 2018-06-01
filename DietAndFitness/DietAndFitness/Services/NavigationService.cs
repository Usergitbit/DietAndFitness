using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietAndFitness.Services
{
    /// <summary>
    /// Class used for navigating from the ViewModel
    /// </summary>
    public class NavigationService : INavigationService
    {
        private NavigationPage navigationPage;
        public NavigationService(NavigationPage navigationPage)
        {
            this.navigationPage = navigationPage;
        }
        public async Task GoBackAsync()
        {
            await navigationPage.PopAsync();
        }

        public async Task NavigateToAsync(Page page)
        {
            await navigationPage.PushAsync(page);
        }

        public async Task PushModal(Page page)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(page);
        }
        public async Task PopModal()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
