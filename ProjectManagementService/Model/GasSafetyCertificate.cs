using System;

namespace PropertyManagementService.Model
{
    public class GasSafetyCertificate
    {
        public int GasCertificateId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}