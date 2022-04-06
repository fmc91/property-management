using PropertyManagementService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementUi.ViewModels
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

        public List<Tenant> Tenants { get; set; }

        public double? OutstandingBalance { get; set; }

        public DateTime? NextPaymentDate { get; set; }

        public DateTime? InsurancePolicyEndDate { get; set; }

        public DateTime? ElectricalInspectionCertificateExpiry { get; set; }

        public DateTime? GasSafetyCertificateExpiry { get; set; }

        public DateTime? EnergyPerformanceCertificateExpiry { get; set; }

        public bool IsOccupied => Tenants != null && Tenants.Count != 0;
    }
}
