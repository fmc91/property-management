using System;

namespace PropertyManagementService.Model
{
    public class InsurancePolicy
    {
        public int InsurancePolicyId { get; set; }

        public int PropertyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Broker Broker { get; set; }

        public Insurer Insurer { get; set; }
    }
}