using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PropertyManagementUi
{
    public class LogicConverter : IMultiValueConverter
    {
        public string Function { get; set; }

        public IValueConverter OutputConverter { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (Function == null)
                throw new InvalidOperationException($"Value of the property {nameof(Function)} cannot be null.");

            var result = ApplyFunction(values);

            return OutputConverter == null ? result : OutputConverter.Convert(result, targetType, parameter, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool ApplyFunction(IEnumerable<object> values) => Function switch
        {
            "AND" => values.Aggregate(true, (x, y) => x && y is true),
            "NAND" => !values.Aggregate(true, (x, y) => x && y is true),
            "OR" => values.Aggregate(false, (x, y) => x || y is true),
            "NOR" => !values.Aggregate(false, (x, y) => x || y is true),
            _ => throw new InvalidOperationException($"Unrecognised function {Function}")
        };
    }
}
