using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Models;
using DietAndFitness.Models.Secondary;
using DietAndFitness.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class CreateUserProfileViewModel : ViewModelBase
    {
        DialogService dialogService;
        private DataAccessLayer DBLocalAccess;
        public ICommand CreateProfileCommand { get; private set; }
        public Profile UserProfile { get; set; }
        public CreateUserProfileViewModel(NavigationService navigationService) : base(navigationService)
        {
            UserProfile = new Profile();
            dialogService = new DialogService();
            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            CreateProfileCommand = new Command<Profile>(execute: CreateUserProfile, canExecute: ValidateCreateButon);
        }
        void CreateUserProfile(Profile parameter)
        {
            if (!Application.Current.Properties.ContainsKey("HasProfiles"))
                Application.Current.Properties.Add("HasProfiles", "True");
        }
        bool ValidateCreateButon(Profile parameter)
        {
            //TODO: check user data
            return true;
        }

       
    }
}
