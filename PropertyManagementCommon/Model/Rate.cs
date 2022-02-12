using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class Rate
    {
        public int RateId { get; set; }

        public int TenancyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Amount { get; set; }
    }
}
