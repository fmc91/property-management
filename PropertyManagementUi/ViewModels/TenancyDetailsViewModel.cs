using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PropertyManagementCommon;
using PropertyManagementCommon.Model;
using PropertyManagementService;

namespace PropertyManagementUi
{
    public class TenancyDetailsViewModel
    {
        public int TenancyId { get; set; }

        public int PropertyId { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public List<Tenant> CurrentTenants { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public DateTime FirstPayment { get; set; }

        public double OutstandingBalance { get; set; }

        public DateTime? NextPaymentDue { get; set; }

        public List<Rate> Rates { get; set; }

        public List<TenancyAccountEntry> AccountEntries { get; set; }

        public double AccountBalance => AccountEntries == null ? 0d : AccountEntries.Sum(e => e.Amount);
    }
}
