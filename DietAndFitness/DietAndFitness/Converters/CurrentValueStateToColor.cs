using DietAndFitness.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DietAndFitness.Converters
{
    public class CurrentValueStateToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Gray;
            var state = (CurrentValuesState)value;
            var result = state switch
            {
                CurrentValuesState.Good => Color.Green,
                CurrentValuesState.Neutral => Color.Yellow,
                CurrentValuesState.Bad => Color.Red,
                _ => Color.Gray,
            };
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
