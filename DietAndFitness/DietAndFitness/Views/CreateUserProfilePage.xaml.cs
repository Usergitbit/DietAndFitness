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
        CreateUserProfileViewModel userProfileViewModel { get; set; }

        public CreateUserProfilePage ()
		{
            
			InitializeComponent ();
            userProfileViewModel = new CreateUserProfileViewModel(App.NavigationService);
            BindingContext = userProfileViewModel;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await userProfileViewModel.LoadData();
        }
        private void OnFormulaPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (formulaPicker.SelectedIndex == 1)
            {
                bodyFatNameLabel.IsVisible = true;
                bodyFatEntry.IsVisible = true;
            }
            else
            {
                bodyFatNameLabel.IsVisible = false;
                bodyFatEntry.IsVisible = false;
            }
        }


    }
}