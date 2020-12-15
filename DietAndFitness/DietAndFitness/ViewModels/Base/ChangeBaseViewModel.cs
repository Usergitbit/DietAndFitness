using DietAndFitness.Core;
using DietAndFitness.Core.Models;
using DietAndFitness.Extensions;
using DietAndFitness.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels.Base
{
    public abstract class ChangeBaseViewModel<T> : ViewModelBase where T : DatabaseEntity, new()
    {
        #region Members
        private T currentItem;

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

                if (currentItem != null)
                    currentItem.PropertyChanged -= OnCurrentItemPropertyChanged;
                currentItem = value;
                if (currentItem != null)
                    currentItem.PropertyChanged += OnCurrentItemPropertyChanged;
                OnPropertyChanged();
            }
        }
        public ICommand ExecuteOperationCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }

        #endregion

        public ChangeBaseViewModel(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService) : base(navigationService, dataAccessService, dialogService)
        {
            CurrentItem = new T();
            Initialize();
        }

        #region Methods
        private void Initialize()
        {
            CurrentItem.PropertyChanged += OnCurrentItemPropertyChanged;
            ExecuteOperationCommand = new Command<T>(async (T param) => { await Operation(param); }, canExecute: ValidateExecuteOperationButton);
            CloseCommand = new Command(async () => { await Close(); });
        }
        private async Task Close()
        {
            await navigationService.PopModal();
        }
        protected abstract Task Operation(T parameter);

        protected virtual void OnOperationSuccess()
        {

        }

        protected virtual void OnOperationFailiure()
        {

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
        private void OnCurrentItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            (ExecuteOperationCommand as Command).ChangeCanExecute();
        }
        #endregion

    }
}
