using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Models;
using DietAndFitness.Models.Secondary;
using DietAndFitness.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
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
        public ObservableCollection<DietFormula> DietFormulas { get; set; }
        public DietFormula SelectedDietFormula { get; set; }
        public CreateUserProfileViewModel(NavigationService navigationService) : base(navigationService)
        {
            ObservableCollection<DietFormula> DietFormulas = new ObservableCollection<DietFormula>();
            UserProfile = new Profile();
            SelectedDietFormula = new DietFormula();
            dialogService = new DialogService();
            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            CreateProfileCommand = new Command<Profile>(execute: CreateUserProfile, canExecute: ValidateCreateButon);
            UserProfile.PropertyChanged += OnUserProfilePropertyChanged;
        }

        public async Task LoadData()
        {
            List<DietFormula> dietFormulas = await DBLocalAccess.GetAllAsync<DietFormula>();
            DietFormulas = new ObservableCollection<DietFormula>(dietFormulas);
        }

        private void OnUserProfilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedDietFormula")
                UserProfile.DietFormula = SelectedDietFormula.ID;
            (CreateProfileCommand as Command).ChangeCanExecute();
        }

        async void CreateUserProfile(Profile parameter)
        {
            if (!Application.Current.Properties.ContainsKey("HasProfiles"))
                Application.Current.Properties.Add("HasProfiles", "True");
            try
            {
               await DBLocalAccess.Insert<Profile>(UserProfile);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message + ex.InnerException + ex.Source + ex.StackTrace);
            }
            navigationService.SetMainPage();
        }
        bool ValidateCreateButon(Profile parameter)
        {
            if(UserProfile.IsValid())
                return true;
            return false;
        }

       
    }
}
