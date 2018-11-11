using DietAndFitness.Core;
using DietAndFitness.ViewModels.Secondary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class CaloriesStatisticsViewModel : ViewModelBase
    {
        private DateTime startDate;
        private DateTime endDate;
        private ObservableCollection<CompleteFoodItem> monthlyCalories;

        public ObservableCollection<CompleteFoodItem> MonthlyCalories
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
            MonthlyCalories = new ObservableCollection<CompleteFoodItem>();
        }

        public async void LoadData()
        {
            MonthlyCalories = new ObservableCollection<CompleteFoodItem>(await DBLocalAccess.GetCompleteItemAsync(StartDate, EndDate));            
        }


    }
}
