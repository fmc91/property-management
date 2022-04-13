using System;
using System.Collections.Generic;
using System.Linq;
using PropertyManagementCommon;

namespace PropertyManagementService.Model
{
    public class Tenancy
    {
        public int TenancyId { get; set; }

        public int PropertyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime FirstPayment { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public double Deposit { get; set; }

        public Agent Agent { get; set; }

        public Property Property { get; set; }

        public List<Tenant> Tenants { get; set; }

        public List<Rate> Rates { get; set; }

        public List<AccountEntry> AccountEntries { get; set; }

        public List<ScheduledPayment> ScheduledPayments { get; set; }

        public double AmountPayable => AccountEntries
            .Where(p => p.Kind != AccountEntryKind.RealisedPayment)
            .Sum(p => p.Amount);

        public double TotalAmountPaid => AccountEntries
            .Where(p => p.Kind == AccountEntryKind.RealisedPayment)
            .Sum(p => p.Amount);

        public double AccountBalance => AccountEntries.Sum(e => e.Amount);

        public double OutstandingBalance => AccountBalance > 0 ? 0 : AccountBalance * -1;

        public DateTime? NextPaymentDate
        {
            get
            {
                double totalPaid = TotalAmountPaid;

                double runningTotal = 0;

                return ScheduledPayments
                    .OrderBy(p => p.Date)
                    .SkipWhile(p => (runningTotal += p.Amount) <= totalPaid || p.Date < DateTime.Today)
                    .Select(p => p.Date)
                    .FirstOrDefault();
            }
        }
    }
}