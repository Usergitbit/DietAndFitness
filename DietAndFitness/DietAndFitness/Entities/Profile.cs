﻿using DietAndFitness.ViewModels.Secondary;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietAndFitness.Entities
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
        private DateTime startDate;
        private DateTime endDate;
        private int? profileTypesId;
        private ProfileType profileTypes;
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
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (startDate == value)
                    return;
                startDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (endDate == value)
                    return;
                endDate = value;
                OnPropertyChanged();
            }
        }
        public int? ProfileTypesId
        {
            get
            {
                return profileTypesId;
            }
            set
            {
                if (profileTypesId == value)
                    return;
                profileTypesId = value;
                OnPropertyChanged();
            }
        }

        public override bool IsValid()
       {
            if (Height <= 0 || Weight <= 0 || String.IsNullOrWhiteSpace(Sex) || String.IsNullOrWhiteSpace(Name) || DietFormula < 0 || StartDate > EndDate || ProfileTypesId == null || DietFormula == null)
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
            ProfileTypesId = 1;

        }
        public Profile()
        {
            ActivityLevel = 1.2;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            BirthDate = new DateTime(1990,1,1);
        }
        public Sum GetTargetValues()
        {
            
            double bmr = 0;
            switch(DietFormula.Value)
            {
                case 1:
                    bmr = Sex == "Male" ? (10 * Weight) + (6.25 * Height) - (5 * (DateTime.Today.Year - BirthDate.Year)) + 5
                                        : (10 * Weight) + (6.25 * Height) - (5 * (DateTime.Today.Year - BirthDate.Year)) - 161;
                    break;
                case 2:
                    //Fat Free Mass = Weight – (Body Fat Percentage * Weight)
                    double ffm = Weight - (BodyFat / 100 * Weight);
                    bmr = 21.6 * ffm + 370;
                    break;
                case 3:
                    bmr = Sex == "Male" ? 66.5 + (13.75 * Weight) + (5.003 * Height) - (6.775 * (DateTime.Today.Year - BirthDate.Year))
                                        : 655.1 + (9.563 * Weight) + (1.85 * Height) - (4.676 * (DateTime.Today.Year - BirthDate.Year));
                    break;
            }
            bmr *= ActivityLevel;
            switch (ProfileTypesId)
            {
                //maintaince
                case (int)ProfileTypeEnum.Maintaince:
                    return new Sum("Target: ")
                    {
                        Calories = bmr,
                        Proteins = Weight * 1.8
                        
                    };
                //cut
                case (int)ProfileTypeEnum.Cut:
                    return new Sum("Target: ")
                    {
                        Calories = bmr / 1.2,
                        Proteins = Weight * 1.8
                    };
                //bulk
                case (int)ProfileTypeEnum.Bulk:
                    return new Sum("Target: ")
                    {
                        Calories = bmr * 1.1,
                        Proteins = Weight * 1.8
                    };
                default:
                    return new Sum("Target: ");
            }
        }
        public ProfileType ProfileTypes
        {
            get { return profileTypes; }
            set
            {
                if (profileTypes == value)
                    return;
                profileTypes = value;
                OnCollectionChanged();
            }
        }
        public Sum GetMaximumValues()
        {
            var targeValues = GetTargetValues();
            targeValues.Prefix = "Maximum: ";
            targeValues.Calories += 100;
            return targeValues;
        }

    }

    public enum ProfileTypeEnum
    {
        Maintaince = 1,
        Cut = 2,
        Bulk = 3
    }
}
