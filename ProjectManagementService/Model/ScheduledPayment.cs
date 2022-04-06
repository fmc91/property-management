using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementService.Model
{
    public class ScheduledPayment
    {
        public int ScheduledPaymentId { get; set; }

        public int TenancyId { get; set; }

        public Tenancy Tenancy { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public bool Processed { get; set; }
    }
}
