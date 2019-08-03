using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Core.Models;
using DietAndFitness.Extensions;
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
        private DietFormula dietFormula;
        private ProfileType profileType;
        private Profile userProfile;
        private string buttonText;
        private bool isPopUpOpen = false;
        private bool isFemale = false;

        private double _NeckLength;
        public double NeckLength
        {
            get { return _NeckLength; }
            set
            {
                if (_NeckLength == value)
                    return;
                _NeckLength = value;
                OnPropertyChanged();
            }
        }

        private double _WaistLenght;
        public double WaistLenght
        {
            get { return _WaistLenght; }
            set
            {
                if (_WaistLenght == value)
                    return;
                _WaistLenght = value;
                OnPropertyChanged();
            }
        }

        private double _HipLength;
        public double HipLength
        {
            get { return _HipLength; }
            set
            {
                if (_HipLength == value)
                    return;
                _HipLength = value;
                OnPropertyChanged();
            }
        }

        private double _CalculatedBodyFat;
        public double CalculatedBodyFat
        {
            get { return _CalculatedBodyFat; }
            set
            {
                if (_CalculatedBodyFat == value)
                    return;
                _CalculatedBodyFat = value;
                OnPropertyChanged();
            }
        }

        public bool IsFemale
        {
            get => isFemale;
            set
            {
                if (isFemale == value)
                    return;
                isFemale = value;
                OnPropertyChanged();
            }
        }

        public bool IsPopUpOpen
        {
            get => isPopUpOpen;
            set
            {
                if (isPopUpOpen == value)
                    return;
                isPopUpOpen = value;
                OnPropertyChanged();
            }
        }
        public string ButtonText
        {
            get
            {
                return buttonText;
            }
            set
            {
                if (buttonText == value)
                    return;
                buttonText = value;
                OnPropertyChanged();
            }
        }
        public ICommand CreateProfileCommand { get; private set; }
        public ICommand CalculateBodyFatCommand { get; private set; }
        public ICommand PopUpAcceptCommand { get; private set; }
        public ICommand PopUpDeclineCommand { get; private set; }
        public ICommand FormulaHelpCommand { get; private set; }
        public ICommand ActivityLevelHelpCommand { get; private set; }
        public Profile UserProfile
        {
            get
            {
                return userProfile;
            }
            set
            {
                if (userProfile == value)
                    return;
                userProfile = value;
                OnPropertyChanged();
            }
        }
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

        public CreateUserProfileViewModel() : base()
        {
            DietFormulas = new ObservableCollection<DietFormula>();
            ProfileTypes = new ObservableCollection<ProfileType>();
            UserProfile = new Profile();
            SelectedDietFormula = new DietFormula();
            SelectedProfileType = new ProfileType();
            DBLocalAccess = new DataAccessService();
            CreateProfileCommand = new Command<Profile>(execute: CreateUserProfile, canExecute: ValidateCreateButon);
            CalculateBodyFatCommand = new Command(execute: OpenCalculateBodyFatDialog);
            PopUpAcceptCommand = new Command(execute: AcceptBodyFatCalculation, canExecute: ValidateAcceptBodyFatCalculationButton);
            FormulaHelpCommand = new Command(execute: ShowFormulaHelpDialog);
            ActivityLevelHelpCommand = new Command(execute: ShowActivityHelpDialog);
            PropertyChanged += OnSelectionChangedIDSolver;
            UserProfile.PropertyChanged += OnUserProfileChanged;
            ButtonText = "Create Profile";
        }

        private async void ShowActivityHelpDialog()
        {
            await dialogService.ShowMessage("1.2 - Sedentary\n1.3 - 1.4 Light(1-3 days/week)\n1.5 - 1.6 Moderate(3-5 days/week)\n1.7 - 1.8 Active(7 days/week)\n1.9 - 2 Athlete", "Activity Level Information");
        }

        private async void ShowFormulaHelpDialog()
        {
            await dialogService.ShowMessage("1. Mifflin – St Jeor Formula - The most accurate according to the American Dietetic Association.\n2. Katch-McArdle - Based on  Mifflin – St Jeor formula and the most accurate but requires a body fat calculation. (Recommended)\n3. Harris-Benedict Formula - old formula that tends to overstate calorie needs by 5%. The results tend to be skewed towards both obese and young people.", "Formula Information");
        }

        private bool ValidateAcceptBodyFatCalculationButton()
        {
            if (!double.IsNaN(CalculatedBodyFat))
                return true;
            else
                return false;
        }

        private void AcceptBodyFatCalculation()
        {
            UserProfile.BodyFat = CalculatedBodyFat;
        }

        private void OpenCalculateBodyFatDialog()
        {
            if (UserProfile.Sex == null || UserProfile.Height == 0 || UserProfile.Weight == 0)
            {
                dialogService.ShowMessage("In order to calculate your body fat % you must first complete the Height, Weight and Sex fields.", "Information");
                return;
            }
            IsPopUpOpen = true;
        }

        private void OnUserProfileChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserProfile.Sex))
            {
                if (UserProfile.Sex == "Female")
                    IsFemale = true;
                else
                    IsFemale = false;
            }
            (CreateProfileCommand as Command).ChangeCanExecute();
        }

        private void OnSelectionChangedIDSolver(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(x => x.SelectedDietFormula))
                UserProfile.DietFormulaId = SelectedDietFormula?.ID;
            if (e.PropertyName == this.GetPropertyName(x => x.SelectedProfileType))
                UserProfile.ProfileTypesId = SelectedProfileType?.ID;
            if (e.PropertyName == nameof(WaistLenght) || e.PropertyName == nameof(NeckLength) || e.PropertyName == nameof(HipLength))
            {
                RecalculateBodyFat();
                (PopUpAcceptCommand as Command).ChangeCanExecute();
            }
        }

        private void RecalculateBodyFat()
        {
            switch (UserProfile.Sex)
            {
                case "Male":
                    if (NeckLength == 0 || WaistLenght == 0)
                        return;
                    CalculatedBodyFat = Math.Round(495 / (1.0324 - .19077 * Math.Log10(WaistLenght - NeckLength) + .15456 * Math.Log10(UserProfile.Height)) - 450);
                    break;
                case "Female":
                    if (NeckLength == 0 || WaistLenght == 0 || HipLength == 0)
                        return;
                    CalculatedBodyFat = Math.Round(495 / (1.29579 - .35004 * Math.Log10(WaistLenght + HipLength - NeckLength) + .22100 * Math.Log10(UserProfile.Height)) - 450);
                    break;
            }
        }

        public async Task LoadData()
        {
            DietFormulas.Clear();
            List<DietFormula> dietFormulas = await DBLocalAccess.GetAllAsync<DietFormula>();
            dietFormulas.ForEach(x => DietFormulas.Add(x));
            ProfileTypes.Clear();
            List<ProfileType> profileTypes = await DBLocalAccess.GetAllAsync<ProfileType>();
            profileTypes.ForEach(x => ProfileTypes.Add(x));
            if (DBLocalAccess.HasProfiles())
            {
                UserProfile = await DBLocalAccess.GetCurrentProfile();
                SelectedDietFormula = dietFormulas.Find(x => x.ID == UserProfile.DietFormulaId);
                SelectedProfileType = profileTypes.Find(x => x.ID == UserProfile.ProfileTypesId);
                ButtonText = "Edit Profile";
            }

        }



        async void CreateUserProfile(Profile parameter)
        {
            if (parameter.ID == null)
            {
                try
                {
                    await DBLocalAccess.Insert<Profile>(UserProfile);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + ex.InnerException + ex.Source + ex.StackTrace);
                }
                navigationService.SetMainPage();
            }
            else
            {
                try
                {
                    await DBLocalAccess.Update<Profile>(UserProfile);
                    await dialogService.ShowMessage("Profile updated successfully", "Success");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message + ex.InnerException + ex.Source + ex.StackTrace);
                }
            }
        }
        bool ValidateCreateButon(Profile parameter)
        {
            if (UserProfile.IsValid())
                return true;
            return false;
        }


    }
}
