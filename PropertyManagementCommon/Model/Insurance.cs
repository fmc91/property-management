using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class Insurance
    {
        public int InsuranceId { get; set; }

        public int PropertyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int BrokerId { get; set; }

        public int InsurerId { get; set; }
    }
}
