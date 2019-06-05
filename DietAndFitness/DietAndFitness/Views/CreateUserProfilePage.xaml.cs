using DietAndFitness.ViewModels;
using Syncfusion.SfRangeSlider.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateUserProfilePage : ContentPage
	{
        public CreateUserProfileViewModel UserProfileViewModel { get; set; }

        public CreateUserProfilePage ()
		{
			InitializeComponent ();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (UserProfileViewModel == null)
            {
                UserProfileViewModel = new CreateUserProfileViewModel();
                BindingContext = UserProfileViewModel;
                await UserProfileViewModel.LoadData();
            }
            else
            {
                await UserProfileViewModel.LoadData();
            }
        }
        private void OnFormulaPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (formulaPicker.SelectedIndex == 1)
            {
                bodyFatNameLabel.IsVisible = true;
                bodyFatEntry.IsVisible = true;
                bodyFatCalculateButton.IsVisible = true;
            }
            else
            {
                bodyFatNameLabel.IsVisible = false;
                bodyFatEntry.IsVisible = false;
                bodyFatCalculateButton.IsVisible = false;
            }
        }


    }
}