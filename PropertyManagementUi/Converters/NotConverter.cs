using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class NotConverter : IValueConverter
    {
        public IValueConverter OutputConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = value is bool b ? !b : null;

            return OutputConverter == null ? result : OutputConverter.Convert(result, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
