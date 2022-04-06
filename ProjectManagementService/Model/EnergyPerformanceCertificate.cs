using System;

namespace PropertyManagementService.Model
{
    public class EnergyPerformanceCertificate
    {
        public int EnergyCertificateId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}