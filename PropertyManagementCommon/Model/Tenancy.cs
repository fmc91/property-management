using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class Tenancy
    {
        public int TenancyId { get; set; }

        public int PropertyId { get; set; }

        public int AgentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime FirstPayment { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public double Deposit { get; set; }

        public bool DepositPaid { get; set; }
    }
}
