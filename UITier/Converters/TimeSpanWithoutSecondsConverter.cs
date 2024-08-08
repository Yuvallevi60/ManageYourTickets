using System;
using System.Globalization;
using System.Windows.Data;

namespace UITier.Converters
{
    // Convert a TimeSpan to a representing string without the seconds
    internal class TimeSpanWithoutSecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.ToString(@"hh\:mm");
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
