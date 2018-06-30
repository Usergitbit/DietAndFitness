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
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected NavigationService navigationService;
        public ViewModelBase()
        {
            //required for serialization
        }
        public ViewModelBase(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;
            handler?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        }
    }
}
