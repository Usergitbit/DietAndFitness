using DietAndFitness.Controls;
using DietAndFitness.Core;
using DietAndFitness.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class CreateUserProfileViewModel : ViewModelBase
    {
        DialogService dialogService;
        private DataAccessLayer DBLocalAccess;
        public CreateUserProfileViewModel(NavigationService navigationService) : base(navigationService)
        {
            dialogService = new DialogService();
        }
    }
}
