﻿using System.Globalization;
using System.Windows.Data;

namespace Sales_Manager.Modules.Converters
{
    internal class NegateBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool val)
            {
                return !val;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
