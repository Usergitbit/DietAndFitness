using DietAndFitness.Core;
using DietAndFitness.Core.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels.Base
{
    public abstract class ChangeBaseViewModel<T> :  ViewModelBase where T : DatabaseEntity, new()
    {
        #region Members
        private T currentItem;
        private string progressindicator = "Waiting for input...";
        private string operationInfo = "Add";
        protected DateTime date = DateTime.Today;
        #endregion
        public string OperationInfo
        {
            get
            {
                return operationInfo;
            }
            set
            {
                if (value?.Equals(operationInfo) ?? true)
                    return;
                operationInfo = value;
                OnPropertyChanged();
            }
        }
        #region Properties
        public T CurrentItem
        {
            get
            {
                return currentItem;
            }
           
            set
            {
                if (value == currentItem)
                    return;
                currentItem = value;
                OnPropertyChanged();

            }
        }
        public ICommand ExecuteOperationCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public string ProgressIndicator
        {
            get
            {
                return progressindicator;
            }

            set
            {
                if (progressindicator == value)
                    return;
                progressindicator = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public ChangeBaseViewModel() : base()
        {
            CurrentItem = new T();
            Initialize();
        }
        public ChangeBaseViewModel(DateTime date) : base()
        {
            this.date = date;
            CurrentItem = Activator.CreateInstance(typeof(T), date) as T;
            Initialize();
        }
        public ChangeBaseViewModel(T selectedItem) : base()
        {
            CurrentItem = selectedItem;        
            OperationInfo = "Edit";
            Initialize();
        }
        #region Methods
        private void Initialize()
        {
            CurrentItem.PropertyChanged += OnCurrentItemPropertyChanged;
            ExecuteOperationCommand = new Command<T>(execute: Operation, canExecute: ValidateExecuteOperationButton);
            CloseCommand = new Command(execute: Close);
        }
        private async void Close()
        {
            DBLocalAccess.DiscardChanges();
            await navigationService.PopModal();
        }
        protected virtual async void Operation(T parameter)
        {
            if (parameter.ID == null)
            {

                if (parameter.IsValid())
                    try
                    {
                        await DBLocalAccess.Insert(parameter);
                        //if program stays here the insert date will be incorrect
                        // we lose the reference when we make a new item so we have to unsubscribe here to prevent memory leaks
                        CurrentItem.PropertyChanged -= OnCurrentItemPropertyChanged;
                        CurrentItem = Activator.CreateInstance(typeof(T), date) as T;
                        //resubscribe for the new item
                        CurrentItem.PropertyChanged += OnCurrentItemPropertyChanged;
                        ProgressIndicator = "Item added successfully!";
                        await SwitchProgressIndicator();
                    }
                    catch (Exception ex)
                    {
                        await dialogService.ShowError(ex, "Error", "Ok", null);
                    }
            }
            else
            {
                if (parameter.IsValid())
                    try
                    {
                        await DBLocalAccess.Update(parameter);
                        //if program stays here the insert date will be incorrect
                        CurrentItem.Clean();
                        ProgressIndicator = "Item added successfully!";
                        (ExecuteOperationCommand as Command).ChangeCanExecute();
                        await SwitchProgressIndicator();
                    }
                    catch (Exception ex)
                    {
                        await dialogService.ShowError(ex, "Error", "Ok", null);
                    }
            }
        }
        protected virtual bool ValidateExecuteOperationButton(T Parameter)
        {
            if (Parameter != null)
            {
                if (Parameter.ID == null)
                    return Parameter.IsValid();
                else
                {
                    return Parameter.IsDirty;
                }
            }
            return false;
        }
        public async Task SwitchProgressIndicator()
        {
            await Task.Delay(2000);
            ProgressIndicator = "Waiting for input...";
        }
        private void OnCurrentItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("I cahnged the can execute for add ");
            (ExecuteOperationCommand as Command).ChangeCanExecute();
        }
        #endregion

    }
}
