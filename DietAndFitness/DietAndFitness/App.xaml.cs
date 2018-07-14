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
        public static NavigationService NavigationService { get; set; }
		public App ()
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTU4NEAzMTM2MmUzMjJlMzBaTHN0NDgrMGNOMlpMV2RvVW1uczFvQmFDOWVXZnlIVkxQMEVseWRyMDVnPQ==");
            InitializeComponent();
            DatabaseController DBGlobalControl = new DatabaseController(GLOBALFOOD_ITEM_DATABASE);
            DBGlobalControl.CopyDatabase();
            DatabaseController DBLocalControl = new DatabaseController(LOCALFOOD_ITEM_DATABASE);
            DBLocalControl.CopyDatabase();
            GlobalSQLiteConnection.ConnectToGlobalDatabaseAsync(DBGlobalControl.DestinationPath);
            GlobalSQLiteConnection.ConnectToLocalDatabaseAsync(DBLocalControl.DestinationPath);
            //If there are no current profiles then open a CreateUserProfile page
            if (Current.Properties.ContainsKey("HasProfiles"))
            {
                var createUserProfilePage = new CreateUserProfilePage();
                var navigationPage = new NavigationPage(createUserProfilePage);
                NavigationService = new NavigationService(navigationPage);
                
                
                
                
                MainPage = navigationPage;
            }
            //Else open the normal HomePage
            else
            {
                var navigationPage = new NavigationPage(new HomePageDetail());
                NavigationService = new NavigationService(navigationPage);
                var homePage = new HomePage();
                homePage.Detail = navigationPage;
                MainPage = homePage;
            }

        }

		protected override async void OnStart ()
		{
            // Handle when your app starts
            Debug.WriteLine("App Started!");
            bool result = await IsUpToDate();
            if(!result)
                await MergeDatabases();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        /// <summary>
        /// Merges the GlobalFoodItem database with the LocalFoodItemDatabase.
        /// </summary>
        private async Task MergeDatabases()
        {
            DataAccessLayer DBGlobalAccess = new DataAccessLayer(GlobalSQLiteConnection.GlobalDatabase);
            DataAccessLayer DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            List<GlobalFoodItem> globalFoodItems = await DBGlobalAccess.GetAllAsync<GlobalFoodItem>();
            foreach (GlobalFoodItem item in globalFoodItems)
            {
                //check if the items are not already in the Local database and insert them if not
                List<LocalFoodItem> searchResult = await DBLocalAccess.GetByGUID<LocalFoodItem>(item.GUID);
                if (searchResult.Count == 0)
                {
                    try
                    {
                        await DBLocalAccess.Insert<LocalFoodItem>(item);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + " YOU DUN GOOFED");
                    }
                }
                //update the items if they already exist
                else
                {
                    try
                    {
                        await DBLocalAccess.Update<LocalFoodItem>(item);
                        Debug.WriteLine("Updated a value!");
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message + " YOU DUN GOOFED");
                    }
                }
            }
        }
        /// <summary>
        /// Determines whether the Local database is up to date
        /// </summary>
        /// <returns>True if up to date. False if not up to date.</returns>
        private async Task<bool> IsUpToDate()
        {
            DataAccessLayer DBGlobalAccess = new DataAccessLayer(GlobalSQLiteConnection.GlobalDatabase);
            DataAccessLayer DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            List<VersionItem> versionLocal = await DBLocalAccess.GetVersion();
            List<VersionItem> versionGlobal = await DBGlobalAccess.GetVersion();
            if (versionLocal[0].Number < versionGlobal[0].Number)
                return false;
            return true;
        }
    }
}
