using DietAndFitness.Core;
using DietAndFitness.Core.Models;
using DietAndFitness.Core.Models.Base;
using DietAndFitness.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using static DietAndFitness.ViewModels.MacrosStatisticsViewModel.Macro;

namespace DietAndFitness.ViewModels
{
    public class MacrosStatisticsViewModel : ViewModelBase
    {
        private ObservableCollection<Macro> macros;

        public ObservableCollection<Macro> Macros
        {
            get
            {
                return macros;
            }
            set
            {
                if (macros == value)
                    return;
                macros = value;
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
                CalculateMacros();
            }
        }

        public MacrosStatisticsViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            Macros = new ObservableCollection<Macro>();
        }
        
        public async Task LoadData()
        {
            var profiles = await DBLocalAccess.UserProfiles.GetAllAsync();
            UserProfiles = new ObservableCollection<Profile>(profiles);
            SelectedProfile = UserProfiles.LastOrDefault();
        }

        private async Task CalculateMacros()
        {
            var items = await DBLocalAccess.DailyFoodItems.GetCompleteItemAsync(SelectedProfile.StartDate, SelectedProfile.EndDate);
            var endDate = SelectedProfile.EndDate < DateTime.Today ? SelectedProfile.EndDate : DateTime.Today;
            var startDate = SelectedProfile.StartDate;
            var days = endDate.Subtract(startDate).TotalDays;

            Macros = new ObservableCollection<Macro>()
            {
                new Macro { Type = MacroType.Carbohydrates, Value = items.Sum(i => i.Carbohydrates) / (days == 0 ? 1 : days)},
                new Macro { Type = MacroType.Proteins, Value = items.Sum(i => i.Proteins) / (days == 0 ? 1 : days)},
                new Macro { Type = MacroType.Fats, Value = items.Sum(i => i.Fats ) / (days == 0 ? 1 : days) }
            };
        }

        public class Macro : ModelBase
        {
            private MacroType type;
            private string typeString;
            public string TypeString
            {
                get
                {
                    return typeString;
                }
                set
                {
                    if (typeString == value)
                        return;
                    typeString = value;
                    OnPropertyChanged();
                }
            }
            private double? value;
            public MacroType Type
            {
                get
                {
                    return type;
                }
                set
                {
                    if (type == value)
                        return;
                    type = value;
                    TypeString = Enum.GetName(typeof(MacroType), value);
                    OnPropertyChanged();
                }
            }
            public double? Value
            {
                get
                {
                    return value;
                }
                set
                {
                    if (this.value == value)
                        return;
                    this.value = value;

                    OnPropertyChanged();
                }
            }
            public enum MacroType
            {
                //IMPORTANT: None is needed because enums are value types and must be initialized so they default to the first value. Without none the setter OnPropertyChanged will never be triggered.
                None,
                Carbohydrates,
                Proteins,
                Fats
            }

        }
    }
}
