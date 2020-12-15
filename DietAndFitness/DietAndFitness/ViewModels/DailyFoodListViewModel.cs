using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Composite;
using DietAndFitness.Interfaces;
using DietAndFitness.Services.Repositories;
using DietAndFitness.ViewModels.Base;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class DailyFoodListViewModel : ListBaseViewModel<CompleteFoodItem>
    {
        private DateTime date;
        private Sum currentValues;
        private Sum targetValues;
        private Sum maximumValues;
        private Profile currentProfile;
        public Profile CurrentProfile
        {
            get
            {
                return currentProfile;
            }
            set
            {
                if (currentProfile == value)
                    return;
                currentProfile = value;
                OnPropertyChanged();
            }
        }

        public string CaloriesValuesInfo
        {
            get
            {
                if (currentProfile != null)
                    return "Calories Left "
                      + (Date == DateTime.Today ? "Today" : ("on " + Date.ToString("dd MMM yyyy")))
                      + ": "
                      + Math.Round((TargetValues?.Calories - CurrentValues?.Calories) ?? 0);
                else
                    return "Calories "
                        + (Date == DateTime.Today ? "Today" : ("on " + Date.ToString("dd MMM yyyy")))
                        +": "
                        + Math.Round(CurrentValues?.Calories ?? 0);
            }
            set
            {

            }
        }
        public Sum MaximumValues
        {
            get
            {
                return maximumValues;
            }
            set
            {
                if (maximumValues == value)
                    return;
                maximumValues = value;
                OnPropertyChanged();
            }
        }
        public CurrentValuesState? ColorIndicator
        {
            get
            {
                return CurrentProfile?.GetCurrentValuesState(CurrentValues);
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public Color ColorIndicatorProteins
        {
            get
            {
                if (CurrentValues.Proteins < TargetValues.Proteins)
                    return Color.Red;
                else
                    return Color.Green;
            }
            set
            {
                OnPropertyChanged();
            }
        }


        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (value == date)
                    return;
                date = value;
                OnPropertyChanged();
            }
        }
        public Sum CurrentValues
        {
            get
            {
                return currentValues;
            }
            set
            {
                if (currentValues == value)
                    return;
                currentValues = value;
                OnPropertyChanged();
            }
        }
        public Sum TargetValues
        {
            get
            {
                return targetValues;
            }
            set
            {
                if (targetValues == value)
                    return;

                if (targetValues != null)
                    targetValues.PropertyChanged -= UpdateCaloriesValuesInfo;

                targetValues = value;

                if (targetValues != null)
                    targetValues.PropertyChanged += UpdateCaloriesValuesInfo;
                OnPropertyChanged();
            }
        }

        public DailyFoodListViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            CurrentValues = new Sum("Current: ");
            TargetValues = new Sum("Target: ");
            MaximumValues = new Sum("Maximum: ");

            Date = DateTime.Today;
        }

        public override Task InitializeAsync(params object[] parameters)
        {
            var date = (DateTime)parameters.ElementAtOrDefault(0);
            Date = date;
            return Task.CompletedTask;
        }

        private void UpdateCaloriesValuesInfo(object sender, PropertyChangedEventArgs e)
        {

        }

        public async override Task LoadList()
        {
            var todayFoodItems = await DBLocalAccess.DailyFoodItems.GetCompleteFoodItems(Date);
            Items.Clear();
            CurrentValues.Reset();

            TargetValues = await GetTargetValues();
            MaximumValues = await GetMaximumValues();
            foreach (var item in todayFoodItems)
            {
                Items.Add(item);
                CurrentValues.Add(item);
            }
            OnPropertyChanged(nameof(ColorIndicator));
            OnPropertyChanged(nameof(ColorIndicatorProteins));
            OnPropertyChanged(nameof(CaloriesValuesInfo));
            //increase gauge size so the current values don't go outside the page
            if (CurrentValues.Calories > MaximumValues.Calories && currentProfile != null)
                MaximumValues.Calories = CurrentValues.Calories + 100;
        }

        public async Task<Sum> GetTargetValues()
        {
            currentProfile = await DBLocalAccess.UserProfiles.GetAsync(x => x.StartDate <= Date && x.EndDate >= Date);
            if (currentProfile == null)
                return CurrentValues;
            return currentProfile.GetTargetValues();
        }

        public async Task<Sum> GetMaximumValues()
        {
            currentProfile = await DBLocalAccess.UserProfiles.GetAsync(x => x.StartDate <= Date && x.EndDate >= Date);
            if (currentProfile == null)
                return CurrentValues;
            return currentProfile.GetMaximumValues();
        }
        protected override async Task OpenAddPageFunction()
        {
            await navigationService.PushModal("ChangeDailyFoodItem", null, Date);
        }
        protected async override Task ExecuteDelete(bool result)
        {
            if (result == true)
            {
                try
                {
                    await DBLocalAccess.DailyFoodItems.DeleteAsync(SelectedItem.DailyFoodItem);
                    //await LoadList();
                    Items.Remove(SelectedItem);
                    CurrentValues.Remove(SelectedItem);
                    OnPropertyChanged(nameof(ColorIndicator));
                    OnPropertyChanged(nameof(CaloriesValuesInfo));
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
            };
        }

        protected override async Task OpenEditPageFunction(CompleteFoodItem parameter)
        {
            await navigationService.PushModal("ChangeDailyFoodItem", SelectedItem.DailyFoodItem.ID, Date);
        }


    }
}
