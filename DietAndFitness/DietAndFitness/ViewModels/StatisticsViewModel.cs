using DietAndFitness.Core;
using DietAndFitness.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DietAndFitness.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly IViewModelFactory viewModelFactory;

        private CaloriesStatisticsViewModel _CaloriesStatisticsViewModel;
        public CaloriesStatisticsViewModel CaloriesStatisticsViewModel
        {
            get { return _CaloriesStatisticsViewModel; }
            set
            {
                if (_CaloriesStatisticsViewModel == value)
                    return;
                _CaloriesStatisticsViewModel = value;
                OnPropertyChanged();
            }
        }


        private MacrosStatisticsViewModel _MacroStatisticsViewModel;
        public MacrosStatisticsViewModel MacroStatisticsViewModel
        {
            get { return _MacroStatisticsViewModel; }
            set
            {
                if (_MacroStatisticsViewModel == value)
                    return;
                _MacroStatisticsViewModel = value;
                OnPropertyChanged();
            }
        }
        public StatisticsViewModel(INavigationService navigationService, 
            IDataAccessService dataAccessService,
            IViewModelFactory viewModelFactory,
            IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            this.viewModelFactory = viewModelFactory;
        }

        public override void Initialize(params object[] parameters)
        {
            CaloriesStatisticsViewModel = (CaloriesStatisticsViewModel) viewModelFactory.Create(typeof(CaloriesStatisticsViewModel));
            MacroStatisticsViewModel = (MacrosStatisticsViewModel) viewModelFactory.Create(typeof(MacrosStatisticsViewModel));
        }

    }
}
