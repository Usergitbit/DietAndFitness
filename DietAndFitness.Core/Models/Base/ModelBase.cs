using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DietAndFitness.Core
{
    /// <summary>
    /// Base model class that others derive from
    /// </summary>
    [Serializable]
    public class ModelBase : INotifyPropertyChanged,
                             INotifyCollectionChanged,
                             IDisposable
    {

        private bool isDirty;
        //Ignores this property when adding to the database, not needed for storage
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
                isDirty = value;
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
            //throw new NotImplementedException();

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            var handler = PropertyChanged;
            if (handler == null)
                return;
            IsDirty = true;
            Debug.WriteLine("PropertyChanged called from" + ToString() + " " + IsDirty);
            handler?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        }

        protected virtual void OnCollectionChanged()
        {
            var handler = CollectionChanged;
            handler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Clean()
        {
            isDirty = false;
            Debug.WriteLine("I was cleaned");
        }
    }
}
