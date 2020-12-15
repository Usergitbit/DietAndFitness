using DietAndFitness.Core;
using DietAndFitness.Core.Models;
using DietAndFitness.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DietAndFitness.ViewModels
{
    public class CaloriesStatisticsViewModel : ViewModelBase
    {
        private ObservableCollection<DailyCalories> monthlyCalories;
        private double? targetCalories;
        private double? averageCalories;
        public double? AverageCalories
        {
            get => averageCalories;
            set
            {
                if (averageCalories == value)
                    return;
                averageCalories = value;
                OnPropertyChanged();
            }
        }
        public double? TargetCalories
        {
            get => targetCalories;
            set
            {
                if (targetCalories == value)
                    return;
                targetCalories = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DailyCalories> MonthlyCalories
        {
            get
            {
                return monthlyCalories;
            }
            set
            {
                if (monthlyCalories == value)
                    return;
                monthlyCalories = value;
                OnPropertyChanged();
            }

        }

        private ObservableCollection<Profile> userProfiles;
        public ObservableCollection<Profile> UserProfiles
        {
            get { return userProfiles; }
            set
            {
                if (userProfiles == value)
                    return;
                userProfiles = value;
                OnPropertyChanged();
            }
        }


        private Profile _SelectedProfile;
        public Profile SelectedProfile
        {
            get { return _SelectedProfile; }
            set
            {
                if (_SelectedProfile == value)
                    return;
                _SelectedProfile = value;
                OnPropertyChanged();
                CalculateCalories();
            }
        }

        public CaloriesStatisticsViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            MonthlyCalories = new ObservableCollection<DailyCalories>();
        }

        public async Task LoadData()
        {
            var profiles = await DBLocalAccess.UserProfiles.GetAllAsync();
            UserProfiles = new ObservableCollection<Profile>(profiles);
            SelectedProfile = UserProfiles.LastOrDefault();
            await CalculateCalories();
        }

        private async Task CalculateCalories()
        {
            var compelteFoodItems = await DBLocalAccess.DailyFoodItems.GetCompleteItemAsync(SelectedProfile.StartDate, SelectedProfile.EndDate);
            MonthlyCalories = new ObservableCollection<DailyCalories>(compelteFoodItems?.GroupBy(x => x.DailyFoodItem.CreatedAt)?.Select(x => new DailyCalories { Calories = x?.Sum(s => s.Calories), CreatedAt = x.Key }));
            TargetCalories = SelectedProfile.GetTargetValues().Calories;
            AverageCalories = Math.Round(MonthlyCalories.Sum(mc => mc.Calories) / (MonthlyCalories.Count == 0 ? 1 : MonthlyCalories.Count) ?? 0);
        }

        public class DailyCalories
        {
            private DateTime createdAt;
            public DateTime CreatedAt
            {
                get
                {
                    return createdAt;
                }
                set
                {
                    if (createdAt == value)
                        return;
                    createdAt = value;
                }
            }

            private double? calories;
            public double? Calories
            {
                get
                {
                    return calories;
                }
                set
                {
                    if (calories == value)
                        return;
                    calories = value;
                }
            }
            public DailyCalories()
            {

            }

        }
    }
}
