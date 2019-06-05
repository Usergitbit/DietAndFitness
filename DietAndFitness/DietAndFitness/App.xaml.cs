using DietAndFitness.Controls;
using DietAndFitness.Entities;
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
using Xamarin.Forms.Xaml;

namespace DietAndFitness
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
	{
        private const string GLOBALFOOD_ITEM_DATABASE = "LocalDatabase.db";
        private const string LOCALFOOD_ITEM_DATABASE = "LocalFoodItemsDB.db";
        private DataAccessLayer DBGlobalAccess = new DataAccessLayer(GlobalSQLiteConnection.GlobalDatabase);
        private DataAccessLayer DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
        public static NavigationService NavigationService { get; set; }
		public App ()
		{

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTc4NjJAMzEzNzJlMzEyZTMwaHNXMVlYNkllK0psbWU4SmFQSjRieWFRWlNxSjJxekpkTXYwLytNaXdVST0=;OTc4NjNAMzEzNzJlMzEyZTMwaUphNlg5RDNySXJwTjlYSmtaeHF4VGlCWFZUckdodGJZTklRSExxNStnST0=;OTc4NjRAMzEzNzJlMzEyZTMwQ0k4dGxoaHpQSlFiRE9NL2lPR2NHOWQzREEvL2VRUkhnM2xTQU0zeUM2ST0=;OTc4NjVAMzEzNzJlMzEyZTMwSFVTYllQVmF5N3lWc0VsSjlwRGVwSXQrTExXd3NHd2hvdUdyKzNsbmUybz0=;OTc4NjZAMzEzNzJlMzEyZTMwU0NseWFqeXpVWTVNV1NGRE9RZnNPK1VTQ2I3Wnowc0cwYzB0bm53Z0k3dz0=;OTc4NjdAMzEzNzJlMzEyZTMwUXZUbEVKSnBoSVFsSFFWaXlJckdVSGxGZUgxcWc5K1VzaWJjUTc3MWlBaz0=;OTc4NjhAMzEzNzJlMzEyZTMwRGlZbEdhaTMyQU1EV1RWazhTU2tkbk5BdmVNTHFXMmdSZVJWMjZ5MVlzND0=;OTc4NjlAMzEzNzJlMzEyZTMwWWlpLy9DdkZHQ0lHUGtKQW1JMkcwMVRBVnNsakkyS01MN0VLWWxUeDZ0Yz0=;OTc4NzBAMzEzNzJlMzEyZTMwbndDZW56NWM0Q3A0U3JyOFNVa21oOThOdWgvWUZ3QTFmZEdlRW9CKzVURT0=;OTc4NzFAMzEzNzJlMzEyZTMwU0NseWFqeXpVWTVNV1NGRE9RZnNPK1VTQ2I3Wnowc0cwYzB0bm53Z0k3dz0=");
            InitializeComponent();
            DatabaseController DBGlobalControl = new DatabaseController(GLOBALFOOD_ITEM_DATABASE);
            DBGlobalControl.CopyDatabase();
            DatabaseController DBLocalControl = new DatabaseController(LOCALFOOD_ITEM_DATABASE);
            DBLocalControl.CopyDatabase();
            GlobalSQLiteConnection.ConnectToGlobalDatabaseAsync(DBGlobalControl.DestinationPath);
            GlobalSQLiteConnection.ConnectToLocalDatabaseAsync(DBLocalControl.DestinationPath);
            GlobalSQLiteConnection.ConnectToLocalDatabase(DBLocalControl.DestinationPath);
            DBGlobalAccess = new DataAccessLayer(GlobalSQLiteConnection.GlobalDatabase);
            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
            //If there are profiles open the normal HomePage
            if (new DataAccessLayer(GlobalSQLiteConnection.LocaDataBaseSync).HasProfiles())//Current.Properties.ContainsKey("HasProfiles"))
            {
                var daily = new DailyFoodListPage();
                var navigationPage = new NavigationPage(daily);
                NavigationService = new NavigationService(navigationPage);
                var vm = new DailyFoodListViewModel();
                daily.BindingContext = vm;
                vm.LoadList();
                var homePage = new HomePage();
                homePage.Detail = navigationPage;
                MainPage = homePage;
            }
            //Else open a CreateUserProfile page
            else
            {
                var createUserProfilePage = new CreateUserProfilePage();
                var navigationPage = new NavigationPage(createUserProfilePage);
                NavigationService = new NavigationService(navigationPage);

                //Setting the VM must be done from outside as the navigation service root page is set to the page itself
                //but the VM requires a reference to the navigation service in the constructor
                CreateUserProfileViewModel userProfileViewModel = new CreateUserProfileViewModel();
                createUserProfilePage.BindingContext = userProfileViewModel;
                createUserProfilePage.UserProfileViewModel = userProfileViewModel;
                MainPage = navigationPage;

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
