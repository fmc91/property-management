using PropertyManagementCommon.Model;
using System;
using System.Text;

namespace PropertyManagementUi
{
    public class InsuranceViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Insurer Insurer { get; set; }

        public Broker Broker { get; set; }
    }
}
