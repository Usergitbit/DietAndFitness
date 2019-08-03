using DietAndFitness.Controls;
using DietAndFitness.Core.Models;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces;
using DietAndFitness.Services;
using DietAndFitness.ViewModels;
using DietAndFitness.Views;
using Microsoft.EntityFrameworkCore;
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
        private IDataAccessService DBLocalAccess = new DataAccessService();
        public static NavigationService NavigationService { get; set; }
		public App ()
		{
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTI1NjUyQDMxMzcyZTMyMmUzMGpjZVl3a0x4dHI1SUoyc0RWcTlYNGFGOWlGSjJyWUJrWjYvTi81U0hJMlU9");
            InitializeComponent();
            DatabaseController DBGlobalControl = new DatabaseController(GLOBALFOOD_ITEM_DATABASE);
            DBGlobalControl.CopyDatabase();
            DatabaseController DBLocalControl = new DatabaseController(LOCALFOOD_ITEM_DATABASE);
            DBLocalControl.CopyDatabase();
            GlobalSQLiteConnection.ConnectToGlobalDatabaseAsync(DBGlobalControl.DestinationPath);
            GlobalSQLiteConnection.ConnectToLocalDatabaseAsync(DBLocalControl.DestinationPath);
            GlobalSQLiteConnection.ConnectToLocalDatabase(DBLocalControl.DestinationPath);
            DBLocalAccess = new DataAccessService();
            IOC.IOC.RegisterDialogService(new DialogService());
            IOC.IOC.RegisterDataAccessService(DBLocalAccess);
            new CompatbilityManager(DBLocalAccess).EnsureCompatibility();
            //If there are profiles open the normal HomePage
            if (DBLocalAccess.HasProfiles())
            {
                var daily = new DailyFoodListPage(false);
                var navigationPage = new NavigationPage(daily);
                NavigationService = new NavigationService(navigationPage);
                IOC.IOC.RegisterNavigationServiceService(NavigationService);
                var vm = new DailyFoodListViewModel();
                daily.DailyFoodDatabase = vm;
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
                IOC.IOC.RegisterNavigationServiceService(NavigationService);
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
            //Debug.WriteLine("App Started!");
            bool result = await IsUpToDate();
            if (!result)
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

            var sqliteContext = new SQLiteDbContext();
            var globalContext = new GlobalDbContext();
            ////add the items that are in the global context but not in the sqlite context
            //sqliteContext.LocalFoodItems
            //    .AddRange(globalContext.GlobalFoodItems
            //                    //select the global food items that are not fount in the local context and transform them into a local food item
            //                    .Where(gfi => sqliteContext.LocalFoodItems
            //                                        .Where(lfi => lfi.GUID == gfi.GUID).Count() == 0)
            //                                        .Select(gfi => new LocalFoodItem(gfi)));

            //foreach(var globalItem in globalContext.GlobalFoodItems)
            //    foreach(var localItem in sqliteContext.LocalFoodItems.Where(lfi => lfi.GUID == globalItem.GUID))
            //    {
            //        localItem.Name = globalItem.Name;
            //        localItem.Proteins = globalItem.Proteins;
            //        localItem.ModifiedAt = DateTime.Now;
            //        localItem.Carbohydrates = globalItem.Carbohydrates;
            //        localItem.Fats = globalItem.Fats;
            //        localItem.Brand = globalItem.Brand;
            //        sqliteContext.LocalFoodItems.Update(localItem);
            //    }
            //await sqliteContext.SaveChangesAsync();

            //List<GlobalFoodItem> globalFoodItems = await DBGlobalAccess.GetAllAsync<GlobalFoodItem>();
            var globalFoodItems = await globalContext.GlobalFoodItems.ToListAsync();
            foreach (var item in globalFoodItems)
            {
                //check if the items are not already in the Local database and insert them if not
                var searchResult = await DBLocalAccess.GetByGUID<LocalFoodItem>(item.GUID);
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
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + " YOU DUN GOOFED");
                    }
                }
            }
            await sqliteContext.SaveChangesAsync();
        }
        /// <summary>
        /// Determines whether the Local database is up to date
        /// </summary>
        /// <returns>True if up to date. False if not up to date.</returns>
        private async Task<bool> IsUpToDate()
        {
            return true;
            List<VersionItem> versionLocal = await new SQLiteDbContext().VersionItems.ToListAsync();
            List<VersionItem> versionGlobal = await new GlobalDbContext().VersionItems.ToListAsync();
            if (versionLocal[0].Number < versionGlobal[0].Number)
                return false;
            return true;
        }
    }
}
