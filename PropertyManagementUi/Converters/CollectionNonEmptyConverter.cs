using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class CollectionNonEmptyConverter : IValueConverter
    {
        public IValueConverter OutputConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value is ICollection c && c.Count != 0;

            return OutputConverter == null ? result : OutputConverter.Convert(result, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
