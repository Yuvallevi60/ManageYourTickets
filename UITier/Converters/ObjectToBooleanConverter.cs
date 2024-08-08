using System;
using System.Globalization;
using System.Windows.Data;

namespace UITier.Converters
{
    // Convert 'null' to 'false' and 'un-null' value to 'true'
    internal class ObjectToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
