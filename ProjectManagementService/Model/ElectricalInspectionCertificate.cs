using System;

namespace PropertyManagementService.Model
{
    public class ElectricalInspectionCertificate
    {
        public int ElectricalCertificateId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}