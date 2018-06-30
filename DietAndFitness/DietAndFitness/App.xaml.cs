using DietAndFitness.Controls;
using DietAndFitness.Models;
using DietAndFitness.Services;
using DietAndFitness.ViewModels;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            if (!Current.Properties.ContainsKey("HasProfiles"))
            {
                var createUserProfilePage = new CreateUserProfilePage();
                CreateUserProfileViewModel userProfileViewModel = new CreateUserProfileViewModel(NavigationService);
                createUserProfilePage.BindingContext = userProfileViewModel;
                var navigationPage = new NavigationPage(createUserProfilePage);
                NavigationService = new NavigationService(navigationPage);
                
                MainPage = navigationPage;
            }
            else
            {
                var navigationPage = new NavigationPage(new HomePageDetail());
                NavigationService = new NavigationService(navigationPage);
                var homePage = new HomePage();
                homePage.Detail = navigationPage;
                MainPage = homePage;
            }

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
        private async Task<bool> ProfileExists()
        {
            var DBAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            List <Profile> profiles = await DBAccess.GetAllAsync<Profile>();
            return profiles.Count != 0;
        }
    }
}
