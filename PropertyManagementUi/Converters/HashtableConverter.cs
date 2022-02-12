using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class HashtableConverter : IValueConverter
    {
        public Hashtable ConversionTable { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConversionTable == null) return null;

            return ConversionTable == null ? null : ConversionTable.ContainsKey(value) ? ConversionTable[value] : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
