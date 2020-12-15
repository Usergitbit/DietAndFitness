using DietAndFitness.Core;
using DietAndFitness.Core.Models;
using DietAndFitness.Interfaces;
using DietAndFitness.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels.Base
{
    public abstract class ListBaseViewModel<T> : ViewModelBase where T : DatabaseEntity, new() 
    {
        #region Members
        private ObservableCollection<T> items;
        private T selectedItem;
        protected RelayCommand confirmDeleteCommand;
        #endregion
        #region Properties
        public ICommand OpenAddPageCommand { get; private set; }
        public ICommand OpenEditPageCommand { get; private set; }
        public T SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem == value)
                    return;
                selectedItem = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<T> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
                OnPropertyChanged();

            }
        }
        public virtual RelayCommand ConfirmDeleteCommand
        {
            get
            {
                return confirmDeleteCommand
                       ?? (confirmDeleteCommand = new RelayCommand(
                           async () =>
                           {
                               await dialogService.ShowMessage("Are you sure you want to delete this item?",
                                  "Warning!",
                                  "Yes",
                                  "No",
                                  ExecuteDelete);
                           }, ValidateDeleteButton));
            }
        }

        protected abstract Task ExecuteDelete(bool result);
        #endregion
        public ListBaseViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            SelectedItem = new T();
            Items = new ObservableCollection<T>();
            OpenAddPageCommand = new Command(async () => await OpenAddPageFunction());
            OpenEditPageCommand = new Command<T>(async (parameter) => await OpenEditPageFunction(parameter), canExecute: ValidateEditButton);
            PropertyChanged += OnSelectedItemChanged;
            
        }
        public override void Dispose()
        {
            PropertyChanged -= OnSelectedItemChanged;
            base.Dispose();
        }
        #region Methods
        protected abstract Task OpenAddPageFunction();
        public abstract Task LoadList();
        protected virtual bool ValidateDeleteButton()
        {
            if (SelectedItem == null || SelectedItem.GetType() == typeof(GlobalFoodItem))
                return false;
            return true;
        }
        protected abstract Task OpenEditPageFunction(T parameter);

        protected virtual bool ValidateEditButton(T parameter)
        {
            if (parameter == null)
                return false;
            else
                return true;
        }
        void OnSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            (ConfirmDeleteCommand as RelayCommand).RaiseCanExecuteChanged();
        }
        #endregion
    }
}
