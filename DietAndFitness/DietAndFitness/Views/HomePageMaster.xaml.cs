using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;
        public Grid LoadingLabel
        {
            get
            {
                return loadingLabel;
            }
        }

        public HomePageMaster()
        {
            InitializeComponent();
            BindingContext = new HomePageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class HomePageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }
            
            public HomePageMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomePageMenuItem>(new[]
                {
                   new HomePageMenuItem { Id = 0, Title = "Today's Food", TargetType = typeof(DailyFoodListPage), IconSource = "today_food_icon.png"},
                    new HomePageMenuItem { Id = 1, Title = "Calendar", TargetType = typeof(CalendarPage), IconSource = "calendar_icon.png"},
                    new HomePageMenuItem { Id = 2, Title = "Food Database", TargetType = typeof(FoodDatabasePage), IconSource = "food_database_icon.png" },
                    new HomePageMenuItem { Id = 3, Title = "Statistics", TargetType = typeof(StatisticsPage), IconSource = "statistics_icon.png" },
                    new HomePageMenuItem { Id = 4, Title = "Options" , TargetType = typeof(OptionsPage), IconSource = "options_icon.png"},
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}