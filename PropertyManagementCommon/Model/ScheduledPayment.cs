using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class ScheduledPayment
    {
        public int ScheduledPaymentId { get; set; }

        public int TenancyId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }
    }
}
