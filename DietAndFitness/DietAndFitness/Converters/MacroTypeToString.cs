using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using static DietAndFitness.ViewModels.MacrosStatisticsViewModel.Macro;

namespace DietAndFitness.Converters
{
    public class MacroTypeToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetName(typeof(MacroType), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
