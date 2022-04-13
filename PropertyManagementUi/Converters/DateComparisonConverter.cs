using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class DateComparisonConverter : IValueConverter
    {
        public string Operator { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Operator == null)
                throw new InvalidOperationException($"The value of the property {nameof(Operator)} cannot be null.");

            if (value is not DateTime dateTime || parameter is not DateTime otherDateTime) return false;

            return Operator switch
            {
                "GREATER" => dateTime > otherDateTime,
                "GREATER-EQUAL" => dateTime >= otherDateTime,
                "LESS" => dateTime < otherDateTime,
                "LESS-EQUAL" => dateTime <= otherDateTime,
                "EQUAL" => dateTime == otherDateTime,
                "NOT-EQUAL" => dateTime != otherDateTime,
                _ => throw new InvalidOperationException($"Unrecognised operator '${Operator}'.")
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
