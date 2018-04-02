using DietAndFitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DietAndFitness
{
    public partial class App : Application
    {
        static FoodItemDatabase database;
        public static FoodItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FoodItemDatabase(DependencyService.Get<IFileFinder>().GetLocalFilePath("FoodItemsSQLite.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            FoodListViewModel F = new FoodListViewModel();
            MainPage = new NavigationPage(new DietAndFitness.LogInPage());
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
