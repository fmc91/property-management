using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementCommon
{
    public static class DateTimeExtensions
    {
        public static int GetQuarter(this DateTime dateTime)
        {
            if (dateTime.Month <= 3)
                return 1;
            else if (dateTime.Month <= 6)
                return 2;
            else if (dateTime.Month <= 9)
                return 3;
            else if (dateTime.Month <= 12)
                return 4;
            else
                throw new ArgumentException("DateTime.Month returned invalid value.");
        }

        public static int LastMonthInQuarter(int quarter) => quarter switch
        {
            1 => 3,
            2 => 6,
            3 => 9,
            4 => 12,
            _ => throw new ArgumentException("Value of argument 'quarter' must be between 1 and 4 inclusive."),
        };
    }
}
