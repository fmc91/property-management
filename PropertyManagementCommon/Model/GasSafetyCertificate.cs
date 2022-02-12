using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementCommon.Model
{
    public class GasSafetyCertificate
    {
        public int GasCertificateId { get; set; }

        public int PropertyId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
