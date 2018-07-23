using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Extensions;
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
        private DietFormula dietFormula;
        private DataAccessLayer DBLocalAccess;
        public ICommand CreateProfileCommand { get; private set; }
        public Profile UserProfile { get; set; }
        public ObservableCollection<DietFormula> DietFormulas { get; set; }
        public DietFormula SelectedDietFormula
        {
            get
            {
                return dietFormula;
            }
            set
            {
                if (dietFormula == value)
                    return;
                dietFormula = value;
                OnPropertyChanged();
            }
        }
        public CreateUserProfileViewModel(NavigationService navigationService) : base(navigationService)
        {
            DietFormulas = new ObservableCollection<DietFormula>();
            UserProfile = new Profile();
            SelectedDietFormula = new DietFormula();
            dialogService = new DialogService();
            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            CreateProfileCommand = new Command<Profile>(execute: CreateUserProfile, canExecute: ValidateCreateButon);
            this.PropertyChanged += OnSelectedDietFormulaChanged;
            this.UserProfile.PropertyChanged += OnUserProfileChanged;
        }

        private void OnUserProfileChanged(object sender, PropertyChangedEventArgs e)
        {
            (CreateProfileCommand as Command).ChangeCanExecute();
        }

        private void OnSelectedDietFormulaChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(x => x.SelectedDietFormula))
                UserProfile.DietFormula = SelectedDietFormula?.ID;
        }

        public async Task LoadData()
        {
            List<DietFormula> dietFormulas = await DBLocalAccess.GetAllAsync<DietFormula>();
            dietFormulas.ForEach(x => DietFormulas.Add(x));
        }

       

        async void CreateUserProfile(Profile parameter)
        {
            if (!Application.Current.Properties.ContainsKey("HasProfiles"))
                Application.Current.Properties.Add("HasProfiles", "True");
            try
            {
               await DBLocalAccess.Insert<Profile>(UserProfile);
                SettingsViewModel.ActiveProfile = UserProfile;
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
