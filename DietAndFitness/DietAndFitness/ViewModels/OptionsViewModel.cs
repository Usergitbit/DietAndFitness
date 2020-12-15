using DietAndFitness.Core;
using DietAndFitness.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {

        private CreateUserProfileViewModel _CreateUserProfileViewModel;
        public CreateUserProfileViewModel CreateUserProfileViewModel
        {
            get { return _CreateUserProfileViewModel; }
            set
            {
                if (_CreateUserProfileViewModel == value)
                    return;
                _CreateUserProfileViewModel = value;
                OnPropertyChanged();
            }
        }


        private UploadDBViewModel _UploadDbViewModel;
        public UploadDBViewModel UploadDbViewModel
        {
            get { return _UploadDbViewModel; }
            set
            {
                if (_UploadDbViewModel == value)
                    return;
                _UploadDbViewModel = value;
                OnPropertyChanged();
            }
        }

        public OptionsViewModel(IViewModelFactory viewModelFactory, INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            CreateUserProfileViewModel = (CreateUserProfileViewModel)viewModelFactory.Create(typeof(CreateUserProfileViewModel));
            UploadDbViewModel = (UploadDBViewModel)viewModelFactory.Create(typeof(UploadDBViewModel));
        }
    }
}
