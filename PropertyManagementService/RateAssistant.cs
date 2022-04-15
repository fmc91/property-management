using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PropertyManagementCommon;
using PropertyManagementService.Model;

namespace PropertyManagementService
{
    public class RateAssistant
    {
        public IEnumerable<ScheduledPayment> CreateScheduledPayments(Tenancy tenancy)
        {
            Func<DateTime, DateTime> increment = tenancy.PaymentFrequency switch
            {
                PaymentFrequency.Weekly => d => d.AddDays(7),
                PaymentFrequency.Monthly => d => d.AddMonths(1),
                PaymentFrequency.Quarterly => d => d.AddMonths(4)
            };

            var result = new List<ScheduledPayment>();

            var prevDate = tenancy.FirstPayment;

            foreach (var rate in tenancy.Rates.OrderBy(r => r.StartDate))
            {
                for (var paymentDate = prevDate; paymentDate < rate.EndDate; paymentDate = increment(paymentDate))
                {
                    if (paymentDate >= rate.StartDate)
                    {
                        result.Add(new ScheduledPayment
                        {
                            Date = paymentDate,
                            Amount = rate.Amount,
                            Processed = false
                        });
                    }

                    prevDate = paymentDate;
                }
            }

            return result;
        }

        public IEnumerable<ScheduledPayment> CreateScheduledPaymentsAfterDate(Tenancy tenancy, DateTime afterDate)
        {
            Func<DateTime, DateTime> increment = tenancy.PaymentFrequency switch
            {
                PaymentFrequency.Weekly => d => d.AddDays(7),
                PaymentFrequency.Monthly => d => d.AddMonths(1),
                PaymentFrequency.Quarterly => d => d.AddMonths(4)
            };

            var result = new List<ScheduledPayment>();

            var prevDate = tenancy.FirstPayment;

            foreach (var rate in tenancy.Rates.Where(r => r.EndDate >= afterDate).OrderBy(r => r.StartDate))
            {
                for (var paymentDate = prevDate; paymentDate < rate.EndDate; paymentDate = increment(paymentDate))
                {
                    if (paymentDate >= rate.StartDate && paymentDate >= afterDate)
                    {
                        result.Add(new ScheduledPayment
                        {
                            Date = paymentDate,
                            Amount = rate.Amount,
                            Processed = false
                        });
                    }

                    prevDate = paymentDate;
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

        public bool HaveRatesChanged(IList<Rate> oldRates, IList<Rate> newRates)
        {
            if (oldRates.Count != newRates.Count) return true;

            for (int i = 0; i < oldRates.Count; i++)
            {
                var rateChanged = oldRates[i].StartDate != newRates[i].StartDate ||
                    oldRates[i].EndDate != newRates[i].EndDate ||
                    oldRates[i].Amount != newRates[i].Amount;

                if (rateChanged) return true;
            }

            return false;
        }

        public bool CheckRatesFallWithinTenancyPeriod(IEnumerable<Rate> rates, DateTime tenancyStartDate, DateTime tenancyEndDate)
        {
            return rates.OrderBy(r => r.StartDate).First().StartDate >= tenancyStartDate &&
                rates.OrderByDescending(r => r.EndDate).First().EndDate <= tenancyEndDate;
        }
    }
}
