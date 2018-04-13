using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
/// <summary>
/// Base model class that others derive from
/// </summary>
namespace DietAndFitness.Core
{
    [Serializable]
    public class ModelBase : INotifyPropertyChanged,
                             INotifyCollectionChanged,
                             IDisposable
    {
        
        private bool isDirty;
        [Ignore] //Ignores this property when adding to the database, not needed for local database at the time of creation
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }
            set
            {
                if (isDirty == value)
                    return;
                isDirty = true;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ModelBase()
        {
            //required for serialization
        }

        public void Dispose()
        {
            throw new NotImplementedException();
            //possibly needed to free resources
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;
            IsDirty = true;
            handler?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        }

        protected virtual void OnCollectionChanged ()
        {
            var handler = this.CollectionChanged;
            handler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
