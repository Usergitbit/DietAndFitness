using DietAndFitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;
using Java.IO;

namespace DietAndFitness
{
    public partial class App : Application
    {
        FoodItemDatabase database;
        public App()
        {
            InitializeComponent();
            //FoodListViewModel F = new FoodListViewModel();

            //database = new FoodItemDatabase(DependencyService.Get<IFileFinder>().GetLocalFilePath("DatabaseTest.db"));
            File.WriteAllBytes();

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
