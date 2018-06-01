using DietAndFitness.Controls;
using DietAndFitness.Services;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace DietAndFitness
{
	public partial class App : Application
	{
        private const string GLOBALFOOD_ITEM_DATABASE = "LocalDatabase.db";
        private const string LOCALFOOD_ITEM_DATABASE = "LocalFoodItemsDB.db";
        public static NavigationService NavigationService { get; private set; }
		public App ()
		{
            InitializeComponent();
            DatabaseController DBGlobalControl = new DatabaseController(GLOBALFOOD_ITEM_DATABASE);
            DBGlobalControl.CopyDatabase();
            DatabaseController DBLocalControl = new DatabaseController(LOCALFOOD_ITEM_DATABASE);
            DBLocalControl.CopyDatabase();
            GlobalSQLiteConnection.ConnectToGlobalDatabaseAsync(DBGlobalControl.DestinationPath);
            GlobalSQLiteConnection.ConnectToLocalDatabaseAsync(DBLocalControl.DestinationPath);
            var navigationPage = new NavigationPage(new HomePageDetail());
            NavigationService = new NavigationService (navigationPage);
            var homePage = new HomePage();
            homePage.Detail = navigationPage;
            MainPage = homePage;
 
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
