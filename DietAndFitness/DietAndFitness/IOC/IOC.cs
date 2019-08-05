using DietAndFitness.Interfaces;
using System;

namespace DietAndFitness.IOC
{
    public static class IOC
    {
        static IDialogService dialogService;
        static INavigationService navigationService;
        static IDataAccessService dataAccessService;
        public static void RegisterDialogService(IDialogService dialogService)
        {
            IOC.dialogService = dialogService;
        }
        public static IDialogService GetDialogService()
        {
            if (dialogService != null)
                return dialogService;
            else
                throw new Exception("Dialog service not registered!");
        }

        public static void RegisterNavigationServiceService(INavigationService navigationService)
        {
            IOC.navigationService = navigationService;
        }
        public static INavigationService GetNavigationService()
        {
            if (navigationService != null)
                return navigationService;
            else
                throw new Exception("Navigation service not registered!");
        }

        public static void RegisterDataAccessService(IDataAccessService dataAccessService)
        {
            IOC.dataAccessService = dataAccessService;
        }
        public static IDataAccessService GetDataAccessService()
        {
            if (dataAccessService != null)
                return dataAccessService;
            else
                throw new Exception("Data access service not registered!");

        }
    }
}