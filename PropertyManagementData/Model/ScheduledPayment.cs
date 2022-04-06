using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementData.Model
{
    public class ScheduledPayment
    {
        public int ScheduledPaymentId { get; set; }

        public int TenancyId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public bool Processed { get; set; }

        public virtual Tenancy Tenancy { get; set; }
    }
}
