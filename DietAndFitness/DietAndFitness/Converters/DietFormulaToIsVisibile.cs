using DietAndFitness.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DietAndFitness.Converters
{
    public class DietFormulaToIsVisibile : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            if (!(value is Profile profile))
                return false;

            if (profile.DietFormulaId == 2 && parameter == null)
                return true;

            if (profile.Sex == "Female" && profile.DietFormulaId == 2 && parameter != null)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
