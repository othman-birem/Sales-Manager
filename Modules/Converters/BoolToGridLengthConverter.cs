using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Sales_Manager.Modules.Converters
{
    class BoolToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                if (val) return new GridLength(80, GridUnitType.Pixel);
            }
            return new GridLength(0, GridUnitType.Pixel);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
