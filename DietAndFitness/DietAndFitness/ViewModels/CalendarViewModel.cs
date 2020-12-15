using DietAndFitness.Core;
using DietAndFitness.Interfaces;
using DietAndFitness.Views;
using System;
using System.Threading.Tasks;
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
        public CalendarViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            SelectedDate = DateTime.Today;
            ViewCommand = new Command(OpenDailyFoodListPage);  
        }

        private async void OpenDailyFoodListPage()
        {
            await navigationService.PushModal("DailyFoodListPage", SelectedDate);
        }


    }
}
