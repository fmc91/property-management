using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementUi
{
    public class PropertySummaryViewModel
    {
        public int PropertyId { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public List<string> Tenants { get; set; }

        public double? OutstandingBalance { get; set; }

        public DateTime? NextPaymentDue { get; set; }

        public DateTime? InsuranceEndDate { get; set; }

        public DateTime? ElectricalInspectionExpiry { get; set; }

        public DateTime? GasSafetyExpiry { get; set; }

        public DateTime? EpcExpiry { get; set; }

        public bool IsOccupied => Tenants != null && Tenants.Count != 0;
    }
}
