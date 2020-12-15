using DietAndFitness.Controls;
using DietAndFitness.Interfaces;
using DietAndFitness.Interfaces.Repositories;
using DietAndFitness.Services;
using DietAndFitness.Services.Repositories;
using DietAndFitness.ViewModels;
using DietAndFitness.Views;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.IOC
{
    public class DefaultIoc : SimpleIoc
    {
        public void Initialize()
        {

            Register<INavigationService, NavigationService>();
            Register<IDataAccessService, DataAccessService>();
            Register<IDialogService, DialogService>();
            Register<IPageViewModelResolver, PageViewModelResolver>();
            Register<IViewModelFactory, ViewModelFactory>();
            Register<IIocProvider, IocProvider>();
            Register<IDailyFoodItemsRepository, DailyFoodItemsRepository>();
            Register<ILocalFoodItemsRepository, LocalFoodItemsRepository>();
            Register<IUserProfilesRepository, UserProfilesRepository>();
            Register<IProfileTypesRepository, ProfileTypesRepository>();
            Register<IDietFormulasRepository, DietFormulasRepository>();

            Register<CalendarViewModel>();
            Register<CaloriesStatisticsViewModel>();
            Register<ChangeDailyFoodItemViewModel>();
            Register<ChangeFoodItemDBViewModel>();
            Register<CreateUserProfileViewModel>();
            Register<DailyFoodListViewModel>();
            Register<FoodDatabaseViewModel>();
            Register<MacrosStatisticsViewModel>();
            Register<UploadDBViewModel>();
            Register<StatisticsViewModel>();
            Register<OptionsViewModel>();

            var pageVmResolver = GetInstance<IPageViewModelResolver>();
            pageVmResolver.Register<DailyFoodListPage, DailyFoodListViewModel>();
            pageVmResolver.Register<CalendarPage, CalendarViewModel>();
            pageVmResolver.Register<ChangeDailyFoodItem, ChangeDailyFoodItemViewModel>();
            pageVmResolver.Register<ChangeFoodItemDB, ChangeFoodItemDBViewModel>();
            pageVmResolver.Register<CreateUserProfilePage, CreateUserProfileViewModel>();
            pageVmResolver.Register<MacrosStatisticsPage, MacrosStatisticsViewModel>();
            pageVmResolver.Register<OptionsPage, OptionsViewModel>();
            pageVmResolver.Register<CaloriesStatisticsPage, CaloriesStatisticsViewModel>();
            pageVmResolver.Register<UploadDbPage, UploadDBViewModel>();
            pageVmResolver.Register<FoodDatabasePage, FoodDatabaseViewModel>();
            pageVmResolver.Register<StatisticsPage, StatisticsViewModel>();

            var navigationService = GetInstance<INavigationService>();
            navigationService.Register<DailyFoodListPage>();
            navigationService.Register<ChangeDailyFoodItem>();
            navigationService.Register<ChangeFoodItemDB>();




        }
    }
}
