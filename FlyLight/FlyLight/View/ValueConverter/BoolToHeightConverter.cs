using System;
using System.Globalization;
using Xamarin.Forms;

namespace FlyLight.View.ValueConverter
{
    public class BoolToHeightValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool) value;

            return boolValue ? 200 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
