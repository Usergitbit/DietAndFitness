using DietAndFitness.Core;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DietAndFitness.ViewModels
{
    public class CaloriesStatisticsViewModel : ViewModelBase
    {
        private DateTime startDate;
        private DateTime endDate;
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
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (startDate == value)
                    return;
                startDate = value;
                OnPropertyChanged();
            }

        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (endDate == value)
                    return;
                endDate = value;
                OnPropertyChanged();
            }
        }

        public CaloriesStatisticsViewModel() : base()
        {
            EndDate = DateTime.Today;
            StartDate = (DateTime.Today).AddMonths(-1);
            MonthlyCalories = new ObservableCollection<DailyCalories>();
        }

        public async Task LoadData()
        {
            var compelteFoodItems = await DBLocalAccess.GetCompleteItemAsync(StartDate, EndDate);
            MonthlyCalories = new ObservableCollection<DailyCalories>(compelteFoodItems?.GroupBy(x => x.DailyFoodItem.CreatedAt)?.Select(x => new DailyCalories { Calories = x?.Sum(s => s.Calories), CreatedAt = x.Key}));
            TargetCalories = (await DBLocalAccess.GetCurrentProfile()).GetTargetValues().Calories;
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
