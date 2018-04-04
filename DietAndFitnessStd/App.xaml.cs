using DietAndFitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;
using System.Reflection;
using System.Diagnostics;

namespace DietAndFitness
{
    public partial class App : Application
    {
        FoodItemDatabase database;
        public App()
        {
            InitializeComponent();

            //TODO Check if database already exists and whether it needs to be updated
            string DestinationPath = null;
            try
            {
               DestinationPath = DependencyService.Get<IFileFinder>().GetLocalFilePath("Databasetest.db");
               using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("DietAndFitness.Resources.Databasetest.db"))
               {
                    using (var destination = File.Create(DestinationPath))
                    {
                        source.CopyTo(destination);
                    }
               }

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message + e.Source +" Error During Database Copying" );
            }
        
            try
            {
                database = new FoodItemDatabase(DependencyService.Get<IFileFinder>().GetLocalFilePath("Databasetest.db"));
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message + "Error at accessing database file");
            }

            FoodListViewModel F = new FoodListViewModel();
            F.LoadList(database.Get());

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
