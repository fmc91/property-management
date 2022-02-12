using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace PropertyManagementUi
{
    public class DateBrushConverter : IValueConverter
    {
        public int AmberThreshold { get; set; }

        public int RedThreshold { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime d))
                return Brushes.Transparent;

            if (DateTime.Today.AddMonths(AmberThreshold) < d)
                return Brushes.Transparent;
            else if (DateTime.Today.AddMonths(RedThreshold) < d)
                return Brushes.Gold;
            else
                return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
