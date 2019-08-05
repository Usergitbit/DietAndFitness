using DietAndFitness.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FoodDatabasePage : ContentPage
	{
        public FoodDatabaseViewModel FoodDatabase { get; set; }
        public FoodDatabasePage ()
		{
            InitializeComponent();
            FoodDatabase = new FoodDatabaseViewModel();
            BindingContext = FoodDatabase;
        }
        protected  override async void OnAppearing()
        {
            base.OnAppearing();
            await FoodDatabase.LoadList();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}