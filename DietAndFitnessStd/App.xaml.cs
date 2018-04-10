using DietAndFitness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xamarin.Forms;
using System.Reflection;
using System.Diagnostics;
using DietAndFitness.ViewModels;
using DietAndFitness.Views;

namespace DietAndFitness
{
    public partial class App : Application
    {
        // public FoodItemDatabase database;
        //public FoodItemsViewModel FoodDatabase;
        //public FoodItemsViewModel mymethod()
        //{
        //    return FoodDatabase;
        //}
        public App()
        {
            InitializeComponent();
         
            string databasename = "LocalDatabase.db";
            //TODO Check if database already exists and whether it needs to be updated
            string DestinationPath = null;
            try
            {
               DestinationPath = DependencyService.Get<IFileFinder>().GetLocalFilePath(databasename);
               using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("DietAndFitness.Resources." + databasename))
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
                 SQLiteConnection.ConnectAsync((DependencyService.Get<IFileFinder>().GetLocalFilePath(databasename)));
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message + "Error at accessing database file");
            }


   
            MainPage = new NavigationPage(new LogInPage());

            
            
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
