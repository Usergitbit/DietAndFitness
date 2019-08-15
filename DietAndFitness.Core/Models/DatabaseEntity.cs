using DietAndFitness.Core.Models.Base;
using DietAndFitness.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace DietAndFitness.Core.Models
{
    public abstract class DatabaseEntity : ModelBase, IValidation
    {
        /// <summary>
        /// Class for an entry in the local database
        /// </summary>
        public int? ID { get; set; }
        public string GUID { get; set; }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value?.Equals(name) ?? true)
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
        public DatabaseEntity()
        {
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
            Deleted = false;
            Name = String.Empty;

        }
        public DatabaseEntity(DateTime date)
        {
            CreatedAt = date;
            ModifiedAt = date;
            Deleted = false;
            Name = string.Empty;

        }
        public abstract bool IsValid();

        public abstract void ResetValues();
        protected override void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            base.OnPropertyChanged(PropertyName);
            ModifiedAt = DateTime.Today;
        }
    }
}
