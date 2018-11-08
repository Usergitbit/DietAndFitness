using DietAndFitness.Core;
using DietAndFitness.Entities;
using DietAndFitness.Extensions;
using DietAndFitness.Services;
using DietAndFitness.ViewModels.Base;
using DietAndFitness.ViewModels.Secondary;
using DietAndFitness.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DietAndFitness.ViewModels
{
    public class DailyFoodListViewModel : ListBaseViewModel<CompleteFoodItem, ChangeDailyFoodItem>
    {
        private DateTime date;
        private Sum currentValues;
        private Sum targetValues;
        private Sum maximumValues;
        private double errorMargin;
        private Profile currentProfile;
        private string dateText;
        public string DateText
        {
            get
            {
                return dateText;
            }
            set
            {
                if (dateText == value)
                    return;
                dateText = value;
                OnPropertyChanged();
            }
        }
        public Profile CurrentProfile
        {
            get
            {
                return currentProfile;
            }
            set
            {
                if (currentProfile == value)
                    return;
                currentProfile = value;
                OnPropertyChanged();
            }
        }

        public Sum MaximumValues
        {
            get
            {
                return maximumValues;
            }
            set
            {
                if (maximumValues == value)
                    return;
                maximumValues = value;
                OnPropertyChanged();
            }
        }
        public Color ColorIndicator
        {
            get
            {
                switch(currentProfile?.ProfileTypesId)
                {
                    case 1:
                        if (CurrentValues.Calories <= TargetValues.Calories + ErrorMargin && CurrentValues.Calories >= TargetValues.Calories - ErrorMargin)
                            return Color.Green;
                        else
                            return Color.Red;
                    case 2:
                        if (CurrentValues.Calories <= TargetValues.Calories)
                            return Color.Green;
                        else
                            return Color.Red;
                    case 3:
                        if (CurrentValues.Calories < TargetValues.Calories)
                            return Color.Red;
                        if (CurrentValues.Calories > TargetValues.Calories + ErrorMargin)
                            return Color.Red;
                        return Color.Green;
                    default:
                        return Color.Gray;
                }
            }
            set
            {
                OnPropertyChanged();
            }
        }
        public double ErrorMargin
        {
            get
            {
                return errorMargin;
            }
            set
            {
                if (errorMargin == value)
                    return;
                errorMargin = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (value == date)
                    return;
                date = value;
                DateText = (date == DateTime.Today ? "Today's" : date.ToString("dd-MMM-yyyy"))  + " Calories";
                OnPropertyChanged();
            }
        }
        public Sum CurrentValues
        {
            get
            {
                return currentValues;
            }
            set
            {
                if (currentValues == value)
                    return;
                currentValues = value;
                OnPropertyChanged();
            }
        }
        public Sum TargetValues
        {
            get
            {
                return targetValues;
            }
            set
            {
                if (targetValues == value)
                    return;
                targetValues = value;
                OnPropertyChanged();
            }
        }
        public DailyFoodListViewModel() : base()
        {
            Initialize();
            Date = DateTime.Today;
        }
        public DailyFoodListViewModel(DateTime date) : base()
        {
            Initialize();
            Date = date;
            SelectedItem.CreatedAt = date;
            SelectedItem.ModifiedAt = date;
        }
        private void Initialize()
        {
            CurrentValues = new Sum("Current: ");
            TargetValues = new Sum("Target: ");
            MaximumValues = new Sum("Maximum: ");
            ErrorMargin = 200;
        }
        public async override void LoadList()
        {
            var todayFoodItems = await DBLocalAccess.GetCompleteItemAsync(Date);
            Items.Clear();
            CurrentValues.Reset();
            currentProfile = await DBLocalAccess.GetCurrentProfile();
            TargetValues = currentProfile.GetTargetValues();
            MaximumValues = currentProfile.GetMaximumValues();
            foreach (var item in todayFoodItems)
            {
                Items.Add(item);
                CurrentValues.Add(item);
                OnPropertyChanged(this.GetPropertyName(x => x.ColorIndicator));
            }
        }
        protected override void OpenAddPageFunction(object[] date)
        {
            base.OpenAddPageFunction(new object[] { Date });
        }
        protected async override void ExecuteDelete(bool result)
        {
            if (result == true)
            {
                try
                {
                    await DBLocalAccess.Delete(SelectedItem.DailyFoodItem);
                    LoadList();
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await dialogService.ShowError(ex, "Error", "Ok", null);
                }
            };
        }
    }
}
