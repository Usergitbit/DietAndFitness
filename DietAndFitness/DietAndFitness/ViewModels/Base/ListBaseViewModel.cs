using DietAndFitness.Core;
using DietAndFitness.Core.Models;
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
    public abstract class ListBaseViewModel<T, P> : ViewModelBase where T : DatabaseEntity, new() where P : ContentPage, new()
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

        protected async virtual Task ExecuteDelete(bool result)
        {
            if (result == true)
            {
                try
                {
                    await DBLocalAccess.Delete((T)SelectedItem);
                    await LoadList();
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
            };
        }
        #endregion
        public ListBaseViewModel() : base()
        {
            SelectedItem = new T();
            Items = new ObservableCollection<T>();
            OpenAddPageCommand = new Command(execute: () => OpenAddPageFunction(null));
            OpenEditPageCommand = new Command<T>(execute: OpenEditPageFunction, canExecute: ValidateEditButton);
            PropertyChanged += OnSelectedItemChanged;
            
        }
        public override void Dispose()
        {
            PropertyChanged -= OnSelectedItemChanged;
            base.Dispose();
        }
        #region Methods
        protected async virtual void OpenAddPageFunction(object[] arguments)
        {
            var addFoodItemPage = (P)Activator.CreateInstance(typeof(P));
            (addFoodItemPage as ContentPage).BindingContext = Activator.CreateInstance(ViewModelLocator.ViewVM[typeof(P)], arguments);
            await navigationService.PushModal(addFoodItemPage as ContentPage);
            SelectedItem = null;
        }
        public virtual async Task LoadList()
        {
            List<T> localFoodItems = new List<T>();
            try
            {
                localFoodItems = await DBLocalAccess.GetAllAsync<T>();
            }
            catch (Exception ex)
            {
                await dialogService.ShowError(ex, "Error", "Ok", null);
            }
            Items.Clear();
            foreach (var item in localFoodItems)
            {
                Items.Add(item);
                await Task.Delay(10);
            }
        }
        protected virtual bool ValidateDeleteButton()
        {
            if (SelectedItem == null || SelectedItem.GetType() == typeof(GlobalFoodItem))
                return false;
            return true;
        }
        protected async virtual void OpenEditPageFunction(T parameter)
        {
            if (parameter != null)
            {
                var editFoodItemPage = (P)Activator.CreateInstance(typeof(P));
                (editFoodItemPage as ContentPage).BindingContext = Activator.CreateInstance(ViewModelLocator.ViewVM[typeof(P)],  SelectedItem);
                await navigationService.PushModal(editFoodItemPage as ContentPage);
            }
            SelectedItem = null;
        }
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
