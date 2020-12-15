using DietAndFitness.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DietAndFitness.Core
{
    /// <summary>
    /// Base ViewModel class that the rest derive from
    /// </summary>
    [Serializable]
    public class ViewModelBase : INotifyPropertyChanged, IDisposable, IInitializable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigationService navigationService;
        protected readonly IDialogService dialogService;
        protected IDataAccessService DBLocalAccess { get; set; }


        public ViewModelBase(INavigationService navigationService, IDataAccessService dataAccessService, IDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.DBLocalAccess = dataAccessService;
            this.dialogService = dialogService;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;
            handler?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        }

        public virtual void Dispose()
        {
            
        }

        public virtual void Initialize(params object[] parameters)
        {

        }

        public virtual Task InitializeAsync(params object[] parameters)
        {
            return Task.CompletedTask;
        }
    }
}
