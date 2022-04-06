using PropertyManagementCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementService.Model
{
    public class Property
    {
        public int PropertyId { get; set; }

        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public string Country { get; set; }

        public DateTime PurchaseDate { get; set; }

        public double PurchasePrice { get; set; }

        public PropertyKind Kind { get; set; }

        public Owner Owner { get; set; }

        public Tenancy Tenancy { get; set; }

        public Image Image { get; set; }

        public List<PurchaseCost> PurchaseCosts { get; set; }

        public List<Improvement> Improvements { get; set; }

        public List<Expense> Expenses { get; set; }

        public InsurancePolicy InsurancePolicy { get; set; }

        public ElectricalInspectionCertificate ElectricalInspectionCertificate { get; set; }

        public GasSafetyCertificate GasSafetyCertificate { get; set; }

        public EnergyPerformanceCertificate EnergyPerformanceCertificate { get; set; }
    }
}
