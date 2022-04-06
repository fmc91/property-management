using PropertyManagementCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PropertyManagementData.Model
{
    public class Property
    {
        public Property()
        {
            Tenancies = new HashSet<Tenancy>();
            PurchaseCosts = new HashSet<PurchaseCost>();
            Improvements = new HashSet<Improvement>();
            Expenses = new HashSet<Expense>();
            InsurancePolicies = new HashSet<InsurancePolicy>();
            ElectricalInspectionCertificates = new HashSet<ElectricalInspectionCertificate>();
            GasSafetyCertificates = new HashSet<GasSafetyCertificate>();
            EnergyPerformanceCertificates = new HashSet<EnergyPerformanceCertificate>();
        }

        public int PropertyId { get; set; }

        public int OwnerId { get; set; }

        [Required]
        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }

        public string Locality { get; set; }

        [Required]
        public string City { get; set; }

        public string County { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime PurchaseDate { get; set; }

        public double PurchasePrice { get; set; }

        public PropertyKind Kind { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Tenancy> Tenancies { get; }

        public virtual ICollection<PurchaseCost> PurchaseCosts { get; }

        public virtual ICollection<Improvement> Improvements { get; }

        public virtual ICollection<Expense> Expenses { get; }

        public virtual ICollection<InsurancePolicy> InsurancePolicies { get; }

        public virtual ICollection<ElectricalInspectionCertificate> ElectricalInspectionCertificates { get; }

        public virtual ICollection<GasSafetyCertificate> GasSafetyCertificates { get; }

        public virtual ICollection<EnergyPerformanceCertificate> EnergyPerformanceCertificates { get; }
    }
}
