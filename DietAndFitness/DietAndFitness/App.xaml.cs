using DietAndFitness.Controls;
using DietAndFitness.Core.Models;
using DietAndFitness.DatabaseContext;
using DietAndFitness.Interfaces;
using DietAndFitness.IOC;
using DietAndFitness.Services;
using DietAndFitness.ViewModels;
using DietAndFitness.Views;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
        private IDataAccessService DBLocalAccess;
        public static DefaultIoc Ioc { get; } = new DefaultIoc();
        public App()
        {
            string license = "";
            using (var source = Assembly.GetExecutingAssembly().GetManifestResourceStream("DietAndFitness.Resources.license.txt"))
            {
                using (var streamReader = new StreamReader(source))
                {
                    license = streamReader.ReadToEnd();
                }
            }
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(license);
            InitializeComponent();
        }

        protected override void OnStart()
        {
            Ioc.Initialize();
            DatabaseController DBGlobalControl = new DatabaseController(GLOBALFOOD_ITEM_DATABASE);
            DBGlobalControl.CopyDatabase();
            DatabaseController DBLocalControl = new DatabaseController(LOCALFOOD_ITEM_DATABASE);
            DBLocalControl.CopyDatabase();
            DBLocalAccess = Ioc.GetInstance<IDataAccessService>();

            //If there are profiles open the normal HomePage
            if (DBLocalAccess.UserProfiles.HasProfiles())
            {
                var daily = new DailyFoodListPage();
                var navigationPage = new NavigationPage(daily);
                var vm = Ioc.GetInstanceWithoutCaching<DailyFoodListViewModel>();
                daily.BindingContext = vm;
                var homePage = new HomePage
                {
                    Detail = navigationPage
                };
                MainPage = homePage;
            }
            //Else open a CreateUserProfile page
            else
            {
                var createUserProfilePage = new CreateUserProfilePage();
                var navigationPage = new NavigationPage(createUserProfilePage);
                //Setting the VM must be done from outside as the navigation service root page is set to the page itself
                //but the VM requires a reference to the navigation service in the constructor
                CreateUserProfileViewModel userProfileViewModel = Ioc.GetInstance<CreateUserProfileViewModel>();
                createUserProfilePage.BindingContext = userProfileViewModel;
                createUserProfilePage.UserProfileViewModel = userProfileViewModel;
                MainPage = navigationPage;
            }

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
