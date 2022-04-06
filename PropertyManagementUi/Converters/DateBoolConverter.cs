using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class DateBoolConverter : IValueConverter
    {
        public bool Inclusive { get; set; }

        public bool InvertLogic { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime d) || !(parameter is DateTime pivotDateTime)) return false;

            return Inclusive ? (InvertLogic ? d <= pivotDateTime : d >= pivotDateTime) :
                (InvertLogic ? d < pivotDateTime : d > pivotDateTime);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
