using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class NonEmptyVisibilityConverter : IValueConverter
    {
        public bool InvertLogic { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is ICollection c && c.Count != 0) ^ InvertLogic ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
