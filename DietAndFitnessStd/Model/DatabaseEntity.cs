using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DietAndFitness.Model
{
    public class DatabaseEntity : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public int? ID { get; set; }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Equals(name))
                    return;
                name = value;
                OnPropertyChanged();
            }
        }
        private DateTime created_at;
        public DateTime CreatedAt
        {
            get
            {
                return created_at;
            }
            set
            {
                if (value.Equals(created_at))
                    return;
                created_at = value;
                OnPropertyChanged();
            }
        }
        private DateTime modified_at;
        public DateTime ModifiedAt
        {
            get
            {
                return modified_at;
            }
            set
            {
                if (value.Equals(modified_at))
                    return;
                modified_at = value;
                OnPropertyChanged();
            }
        }
        private bool deleted;
        public bool Deleted
        {
            get
            {
                return deleted;
            }
            set
            {
                if (value.Equals(deleted))
                    return;
                deleted = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DatabaseEntity()
        {
            CreatedAt = DateTime.Now;
            ModifiedAt = DateTime.Now;
        }
    }
}
