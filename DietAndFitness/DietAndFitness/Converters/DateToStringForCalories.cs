using System;
using System.Globalization;
using Xamarin.Forms;

namespace DietAndFitness.Converters
{
    public class DateToStringForCalories : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return ((DateTime)value == DateTime.Today ? "Today's" : ((DateTime)value).ToString("dd-MMM-yyyy")) + " Calories";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
