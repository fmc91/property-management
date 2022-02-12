using System;
using System.Collections.Generic;
using System.Linq;
using PropertyManagementCommon;
using PropertyManagementCommon.Model;

namespace PropertyManagementService
{
    public class RentAssistant
    {
        private PropertyService _propertyService;

        public RentAssistant(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public double GetRentPayableToDate(int tenancyId)
        {
            return _propertyService.GetScheduledPayments(tenancyId)
                .Where(p => p.Date <= DateTime.Today)
                .Sum(p => p.Amount);
        }

        public double GetTotalAmountPaid(int tenancyId)
        {
            return _propertyService.GetActualPayments(tenancyId)
                .Sum(p => p.Amount);
        }

        public double GetAccountBalance(int tenancyId)
        {
            return GetTotalAmountPaid(tenancyId) - GetRentPayableToDate(tenancyId);
        }

        public DateTime? GetNextPaymentDate(int tenancyId)
        {
            double totalPaid = GetTotalAmountPaid(tenancyId);

            double runningTotal = 0;

            var payments = _propertyService.GetScheduledPayments(tenancyId)
                .OrderBy(p => p.Date)
                .SkipWhile(p => (runningTotal += p.Amount) <= totalPaid || p.Date < DateTime.Today)
                .Select(p => p.Date);

            return payments.Any() ? payments.First() : (DateTime?)null;
        }

        public List<TenancyAccountEntry> GetAllAccountEntries(int tenancyId)
        {
            var scheduledPayments = _propertyService.GetScheduledPayments(tenancyId)
                .Where(p => p.Date <= DateTime.Today)
                .Select(p => new TenancyAccountEntry
                {
                    Date = p.Date,
                    Amount = p.Amount * -1,
                    Narration = $"Rent due {p.Date:dd/MM/yyyy}"
                });

            var actualPayments = _propertyService.GetActualPayments(tenancyId)
                .Where(p => p.Date <= DateTime.Today)
                .Select(p => new TenancyAccountEntry
                {
                    Date = p.Date,
                    Amount = p.Amount,
                    Narration = p.Narration
                });

            var result = new List<TenancyAccountEntry>();

            result.AddRange(scheduledPayments);
            result.AddRange(actualPayments);

            return result;
        }
    }
}
