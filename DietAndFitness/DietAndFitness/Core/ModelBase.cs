using DietAndFitness.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DietAndFitness.Core
{
    public class DailyFoodObservableCollection : ObservableCollection<DailyFoodItem>, INotifyPropertyChanged
    {

        private double _TotalCalories;
        public double TotalCalories
        {
            get { return _TotalCalories; }
            private set
            {
                if (_TotalCalories == value)
                    return;
                _TotalCalories = value;
                RaisePropertyChanged();
            }
        }


        private double _TotalProteins;
        public double TotalProteins
        {
            get { return _TotalProteins; }
            private set
            {
                if (_TotalProteins == value)
                    return;
                _TotalProteins = value;
                RaisePropertyChanged();
            }
        }


        private double _TotalFats;
        public double TotalFats
        {
            get { return _TotalFats; }
            private set
            {
                if (_TotalFats == value)
                    return;
                _TotalFats = value;
                RaisePropertyChanged();
            }
        }


        private double _TotalCarbohydrates;
        public double TotalCarbohydrates
        {
            get { return _TotalCarbohydrates; }
            private set
            {
                if (_TotalCarbohydrates == value)
                    return;
                _TotalCarbohydrates = value;
                RaisePropertyChanged();
            }
        }
        public DailyFoodObservableCollection() : base()
        {
            Initialize();
        }

        public DailyFoodObservableCollection(List<DailyFoodItem> items) : base(items)
        {
            Initialize(items);
        }

        public DailyFoodObservableCollection(IEnumerable<DailyFoodItem> items) : base(items)
        {
            Initialize(items);
        }

        private void Initialize(IEnumerable<DailyFoodItem> items = null)
        {
            if (items?.Any(x => x.FoodItem == null) ?? false)
                throw new Exception("FoodItem prop can't be null for the collection to work");

            CollectionChanged += OnFoodItemsChanged;
        }
        private void OnFoodItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            double totalCalories = 0, totalCarbohydrates = 0, totalProteins = 0, totalFats = 0;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        var dailyFoodItem = item as DailyFoodItem;
                        totalCalories += (dailyFoodItem.FoodItem.Calories ?? 0) * dailyFoodItem.Quantity;
                        totalCarbohydrates += (dailyFoodItem.FoodItem.Carbohydrates ?? 0) * dailyFoodItem.Quantity;
                        totalProteins += (dailyFoodItem.FoodItem.Proteins ?? 0) * dailyFoodItem.Quantity;
                        totalFats += (dailyFoodItem.FoodItem.Fats ?? 0) * dailyFoodItem.Quantity;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach(var item in e.OldItems)
                    {
                        var dailyFoodItem = item as DailyFoodItem;
                        totalCalories -= (dailyFoodItem.FoodItem.Calories ?? 0) * dailyFoodItem.Quantity;
                        totalCarbohydrates -= (dailyFoodItem.FoodItem.Carbohydrates ?? 0) * dailyFoodItem.Quantity;
                        totalProteins -= (dailyFoodItem.FoodItem.Proteins ?? 0) * dailyFoodItem.Quantity;
                        totalFats -= (dailyFoodItem.FoodItem.Fats ?? 0) * dailyFoodItem.Quantity;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (var item in e.OldItems)
                    {
                        var dailyFoodItem = item as DailyFoodItem;
                        totalCalories -= (dailyFoodItem.FoodItem.Calories ?? 0) * dailyFoodItem.Quantity;
                        totalCarbohydrates -= (dailyFoodItem.FoodItem.Carbohydrates ?? 0) * dailyFoodItem.Quantity;
                        totalProteins -= (dailyFoodItem.FoodItem.Proteins ?? 0) * dailyFoodItem.Quantity;
                        totalFats -= (dailyFoodItem.FoodItem.Fats ?? 0) * dailyFoodItem.Quantity;
                    }
                    foreach (var item in e.NewItems)
                    {
                        var dailyFoodItem = item as DailyFoodItem;
                        totalCalories += (dailyFoodItem.FoodItem.Calories ?? 0) * dailyFoodItem.Quantity;
                        totalCarbohydrates += (dailyFoodItem.FoodItem.Carbohydrates ?? 0) * dailyFoodItem.Quantity;
                        totalProteins += (dailyFoodItem.FoodItem.Proteins ?? 0) * dailyFoodItem.Quantity;
                        totalFats += (dailyFoodItem.FoodItem.Fats ?? 0) * dailyFoodItem.Quantity;
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    TotalCalories = 0;
                    TotalCarbohydrates = 0;
                    TotalProteins = 0;
                    TotalFats = 0;
                    break;

            }

            TotalCalories = totalCalories;
            TotalCarbohydrates = totalCarbohydrates;
            TotalProteins = totalProteins;
            TotalFats = totalFats;
        }

        private void RaisePropertyChanged([CallerMemberName] string property = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(property)));
        }

        


    }
}
