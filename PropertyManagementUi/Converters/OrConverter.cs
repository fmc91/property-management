using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class OrConverter : IMultiValueConverter
    {
        public bool InvertOutput { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Aggregate(false, (x, y) => x || y is true) ^ InvertOutput;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
