using DietAndFitness.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUserProfilePage : ContentPage
    {
        public CreateUserProfileViewModel UserProfileViewModel { get; set; }

        public CreateUserProfilePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                if (UserProfileViewModel == null)
                {
                    //UserProfileViewModel = new CreateUserProfileViewModel();
                    UserProfileViewModel = (BindingContext as CreateUserProfileViewModel);
                    await UserProfileViewModel.LoadData();
                }
                else
                {
                    await UserProfileViewModel.LoadData();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "Ok");
            }
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