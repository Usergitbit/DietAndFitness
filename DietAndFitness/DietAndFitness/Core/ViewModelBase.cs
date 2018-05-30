using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
/// <summary>
/// Base ViewModel class that the rest derive from
/// </summary>
namespace DietAndFitness.Core
{
    [Serializable]
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelBase()
        {
            //required for serialization
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
