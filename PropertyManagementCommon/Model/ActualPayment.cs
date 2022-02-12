using System;

namespace PropertyManagementCommon.Model
{
    public class ActualPayment
    {
        public int ActualPaymentId { get; set; }

        public int TenancyId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public string Narration { get; set; }
    }
}