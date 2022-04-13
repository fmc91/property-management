using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class BoolVisibilityConverter : IValueConverter
    {
        public IValueConverter OutputConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (value is bool b && b) ? Visibility.Visible : Visibility.Collapsed;

            return OutputConverter == null ? result : OutputConverter.Convert(result, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
