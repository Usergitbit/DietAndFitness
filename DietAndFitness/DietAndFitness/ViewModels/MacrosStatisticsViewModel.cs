using DietAndFitness.Core;
using DietAndFitness.Core.Models.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static DietAndFitness.ViewModels.MacrosStatisticsViewModel.Macro;

namespace DietAndFitness.ViewModels
{
    public class MacrosStatisticsViewModel : ViewModelBase
    {
        private DateTime startDate;
        private DateTime endDate;
        private ObservableCollection<Macro> macros;

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

        public MacrosStatisticsViewModel()
        {
            Macros = new ObservableCollection<Macro>();
            EndDate = DateTime.Today;
            StartDate = EndDate.AddMonths(-1);
        }
        
        public async void LoadData()
        {
            var items = await DBLocalAccess.GetCompleteItemAsync(StartDate, EndDate);
            Macros = new ObservableCollection<Macro>()
            {
                new Macro { Type = MacroType.Carbohydrates, Value = items.Sum(i => i.Carbohydrates)},
                new Macro { Type = MacroType.Proteins, Value = items.Sum(i => i.Proteins)},
                new Macro { Type = MacroType.Fats, Value = items.Sum(i => i.Fats )}
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
