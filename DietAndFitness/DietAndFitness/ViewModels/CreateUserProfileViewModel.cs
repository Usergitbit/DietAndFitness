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
        private DialogService dialogService;
        private DietFormula dietFormula;
        private ProfileType profileType;
        private DataAccessLayer DBLocalAccess;
        public ICommand CreateProfileCommand { get; private set; }
        public Profile UserProfile { get; set; }
        public ObservableCollection<DietFormula> DietFormulas { get; set; }
        public ObservableCollection<ProfileType> ProfileTypes { get; set; }
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
        public ProfileType SelectedProfileType
        {
            get
            {
                return profileType;
            }
            set
            {
                if (profileType == value)
                    return;
                profileType = value;
                OnPropertyChanged();
            }
        }

        public CreateUserProfileViewModel(NavigationService navigationService) : base(navigationService)
        {
            DietFormulas = new ObservableCollection<DietFormula>();
            ProfileTypes = new ObservableCollection<ProfileType>();
            UserProfile = new Profile();
            SelectedDietFormula = new DietFormula();
            SelectedProfileType = new ProfileType();
            dialogService = new DialogService();
            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            CreateProfileCommand = new Command<Profile>(execute: CreateUserProfile, canExecute: ValidateCreateButon);
            PropertyChanged += OnSelectionChangedIDSolver;
            UserProfile.PropertyChanged += OnUserProfileChanged;
        }

        private void OnUserProfileChanged(object sender, PropertyChangedEventArgs e)
        {
            (CreateProfileCommand as Command).ChangeCanExecute();
        }

        private void OnSelectionChangedIDSolver(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(x => x.SelectedDietFormula))
                UserProfile.DietFormula = SelectedDietFormula?.ID;
            if (e.PropertyName == this.GetPropertyName(x => x.SelectedProfileType))
                UserProfile.ProfileTypesId = SelectedProfileType?.ID;
        }
        public async Task LoadData()
        {
            List<DietFormula> dietFormulas = await DBLocalAccess.GetAllAsync<DietFormula>();
            dietFormulas.ForEach(x => DietFormulas.Add(x));
            List<ProfileType> profileTypes = await DBLocalAccess.GetAllAsync<ProfileType>();
            profileTypes.ForEach(x => ProfileTypes.Add(x));
        }



        async void CreateUserProfile(Profile parameter)
        {
            try
            {
                await DBLocalAccess.Insert<Profile>(UserProfile);
                SettingsViewModel.ActiveProfile = UserProfile;
            }
            catch (Exception ex)
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
