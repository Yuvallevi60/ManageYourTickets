using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UITier.Converters
{
    // Convert 'false' to 'Visibility.Visible' and 'true' to 'Visibility.Collapsed'
    internal class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && !boolValue)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
