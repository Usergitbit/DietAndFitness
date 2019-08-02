using DietAndFitness.Controls;
using DietAndFitness.Interfaces;
using DietAndFitness.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
namespace DietAndFitness.Core
{
    /// <summary>
    /// Base ViewModel class that the rest derive from
    /// </summary>
    [Serializable]
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigationService navigationService;
        protected readonly IDialogService dialogService;
        protected IDataAccessService DBLocalAccess { get; set; }


        public ViewModelBase()
        {
            navigationService = IOC.IOC.GetNavigationService();
            dialogService = IOC.IOC.GetDialogService();
            DBLocalAccess = IOC.IOC.GetDataAccessService();
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
    }
}
