using System;
using System.Collections.Generic;
using System.Text;
using PropertyManagementService.Model;

namespace PropertyManagementUi.ViewModels
{
    public class TenancySummaryViewModel
    {
        public int TenancyId { get; set; }

        public double OutstandingBalance { get; set; }

        public DateTime NextPaymentDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Tenant> Tenants { get; set; }
    }
}
