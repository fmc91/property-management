using System;
using System.Collections.Generic;
using System.Text;
using PropertyManagementCommon;

namespace PropertyManagementData.Model
{
    public class AccountEntry
    {
        public int AccountEntryId { get; set; }

        public int TenancyId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public AccountEntryKind Kind { get; set; }
    }
}
