using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class EnergyPerformanceCertificate
    {
        public int EnergyCertificateId { get; set; }

        public int PropertyId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
