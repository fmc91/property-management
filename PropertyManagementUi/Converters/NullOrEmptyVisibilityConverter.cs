using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class NullOrEmptyVisibilityConverter : IValueConverter
    {
        public bool InvertLogic { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return InvertLogic ? Visibility.Visible : Visibility.Collapsed;

            return (value is string s && s == String.Empty) ^ InvertLogic ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
