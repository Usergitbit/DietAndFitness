using DietAndFitness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DietAndFitness.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DailyFoodListPage : ContentPage
	{
        public DailyFoodListViewModel DailyFoodDatabase { get; set; }
		public DailyFoodListPage ()
		{
			InitializeComponent ();
            DailyFoodDatabase = new DailyFoodListViewModel();
            BindingContext = DailyFoodDatabase;
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DailyFoodDatabase.LoadList();
           
        }
    }
}