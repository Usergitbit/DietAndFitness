using DietAndFitness.Controls;
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
        protected NavigationService navigationService;
        protected readonly IDialogService dialogService;
        protected DataAccessLayer DBLocalAccess { get; set; }


        public ViewModelBase()
        {
            //this.navigationService = navigationService;
            this.navigationService = App.NavigationService;
            dialogService = new DialogService();
            DBLocalAccess = new DataAccessLayer(GlobalSQLiteConnection.LocalDatabase);
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
