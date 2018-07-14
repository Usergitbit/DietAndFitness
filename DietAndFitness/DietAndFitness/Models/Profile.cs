using DietAndFitness.Models.Secondary;
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
        private int? dietFormula;
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
        public int? DietFormula
        {
            get { return dietFormula; }
            set
            {
                if (dietFormula == value)
                    return;
                dietFormula = value;
                //required because of 0 based indexed in picker but FK in database c
                dietFormula++;
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
        public override bool IsValid()
        {
            if (Height <= 0 || Weight <= 0 || String.IsNullOrWhiteSpace(Sex) || String.IsNullOrWhiteSpace(Name) || DietFormula < 0)
                return false;
            return true;
        }

        public override void ResetValues()
        {
            Name = String.Empty;
            CreatedAt = DateTime.Today;
            ModifiedAt = DateTime.Today;
            Height = 0;
            Weight = 0;
            BirthDate = DateTime.Today;
            Sex = String.Empty;
            DietFormula = 1;
            ActivityLevel = 1.2;
            BodyFat = 0;

        }
        public Profile()
        {

        }


    }
}
