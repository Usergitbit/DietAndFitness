using DietAndFitness.Core;
using DietAndFitness.Services;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class CalendarViewModel : ViewModelBase
    {
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }
            set
            {
                if (selectedDate == value)
                    return;
                selectedDate = value;
                OnPropertyChanged();
            }
        }
        public ICommand ViewCommand { get; private set; }
        public CalendarViewModel()
        {
            SelectedDate = DateTime.Today;
            ViewCommand = new Command(OpenDailyFoodListPage);
        }

        private async void OpenDailyFoodListPage()
        {
            var page = new DailyFoodListPage(SelectedDate);
            await navigationService.NavigateToAsync(page);
        }


    }
}
