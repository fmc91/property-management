using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PropertyManagementCommon;
using PropertyManagementCommon.Model;

namespace PropertyManagementService
{
    public class RateAssistant
    {
        public IEnumerable<ScheduledPayment> CreateScheduledPayments(int tenancyId, IEnumerable<Rate> rates, DateTime firstPayment, PaymentFrequency frequency)
        {
            Func<DateTime, DateTime> increment = frequency switch
            {
                PaymentFrequency.Weekly => d => d.AddDays(7),
                PaymentFrequency.Monthly => d => d.AddMonths(1),
                PaymentFrequency.Quarterly => d => d.AddMonths(4)
            };

            List<ScheduledPayment> result = new List<ScheduledPayment>();

            foreach (var rate in rates.OrderBy(r => r.StartDate))
            {
                DateTime paymentDate = firstPayment;

                while (paymentDate < rate.StartDate)
                    paymentDate = increment(paymentDate);

                while (paymentDate <= rate.EndDate)
                {
                    result.Add(new ScheduledPayment
                    {
                        TenancyId = tenancyId,
                        Date = paymentDate,
                        Amount = rate.Amount
                    });

                    paymentDate = increment(paymentDate);
                }
            }

            return result;
        }

        public bool CheckRatesAreContiguous(IEnumerable<Rate> rates)
        {
            Rate prev = null;

            foreach (var current in rates.OrderBy(r => r.StartDate))
            {
                if (prev != null)
                {
                    if (current.StartDate != prev.EndDate.AddDays(1)) return false;
                }

                prev = current;
            }

            return true;
        }

        public bool CheckRatesFallWithinTenacyPeriod(IEnumerable<Rate> rates, DateTime tenancyStartDate, DateTime tenancyEndDate)
        {
            return rates.OrderBy(r => r.StartDate).First().StartDate >= tenancyStartDate &&
                rates.OrderByDescending(r => r.EndDate).First().EndDate <= tenancyEndDate;
        }
    }
}
