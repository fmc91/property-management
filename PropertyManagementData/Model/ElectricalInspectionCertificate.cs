using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementData.Model
{
    public class ElectricalInspectionCertificate
    {
        public int ElectricalCertificateId { get; set; }

        public int PropertyId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
