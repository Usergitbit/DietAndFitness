using DietAndFitness.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                //crashes on UWP if not created on main thread
                if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.BeginInvokeOnMainThread(new Action(() =>
                    {
                        InitializeComponent();
                    }));
                }
                else
                {
                    InitializeComponent();
                }
                DailyFoodDatabase = new DailyFoodListViewModel();
                BindingContext = DailyFoodDatabase;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("from on first ctr");
            }
        }
        public DailyFoodListPage(DateTime date)
        {
            try
            {
                InitializeComponent();
                DailyFoodDatabase = new DailyFoodListViewModel(date);
                BindingContext = DailyFoodDatabase;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("from on 2nd ctr");
            }
        }
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                DailyFoodDatabase.LoadList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine("from on appearing");
            }


        }
    }
}