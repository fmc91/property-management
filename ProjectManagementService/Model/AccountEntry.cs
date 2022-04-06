using System;
using PropertyManagementCommon;

namespace PropertyManagementService.Model
{
    public class AccountEntry
    {
        public int AccountEntryId { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public AccountEntryKind Kind { get; set; }
    }
}