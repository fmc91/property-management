using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementData.Model
{
    public class InsurancePolicy
    {
        public int InsurancePolicyId { get; set; }

        public int PropertyId { get; set; }

        public int BrokerId { get; set; }

        public int InsurerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public virtual Broker Broker { get; set; }

        public virtual Insurer Insurer { get; set; }
    }
}
