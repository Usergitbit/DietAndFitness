using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Models
{
    [Table("Profiles")]
    public class Profile : DatabaseEntity
    {
        private double height;
        private double weight;
        private DateTime birthDate;
        private string sex;
        private int dietFormula;
        private double activityLevel;
        private double bodyFat;

        public double Height
        {
            get { return height; }
            set
            {
                if (height == value)
                    return;
                height = value;
                OnPropertyChanged();
            }
        }
        public double Weight
        {
            get { return weight; }
            set
            {
                if (weight == value)
                    return;
                weight = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate == value)
                    return;
                birthDate = value;
                OnPropertyChanged();
            }
        }
        public string Sex
        {
            get { return sex; }
            set
            {
                if (sex == value)
                    return;
                sex = value;
                OnPropertyChanged();
            }
        }
        public int DietFormula
        {
            get { return dietFormula; }
            set
            {
                if (dietFormula == value)
                    return;
                dietFormula = value;
                OnPropertyChanged();
            }
        }
        public double ActivityLevel
        {
            get { return activityLevel; }
            set
            {
                if (activityLevel == value)
                    return;
                activityLevel = value;
                OnPropertyChanged();
            }
        }
        public double BodyFat
        {
            get { return bodyFat; }
            set
            {
                if (bodyFat == value)
                    return;
                bodyFat = value;
                OnPropertyChanged();
            }
        }
        public override bool Check()
        {
            throw new NotImplementedException();
        }

        public override void ResetValues()
        {
            throw new NotImplementedException();
        }
    }
}
