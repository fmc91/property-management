using PropertyManagementCommon.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementUi
{
    public class TenancyViewModel
    {
        public int TenancyId { get; set; }

        public double? OutstandingBalance { get; set; }

        public DateTime? NextPaymentDue { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<Tenant> Tenants { get; set; }
    }
}
