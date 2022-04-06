using PropertyManagementCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementData.Model
{
    public class Tenancy
    {
        public Tenancy()
        {
            Tenants = new HashSet<Tenant>();
            Rates = new HashSet<Rate>();
            AccountEntries = new HashSet<AccountEntry>();
            ScheduledPayments = new HashSet<ScheduledPayment>();
        }

        public int TenancyId { get; set; }

        public int PropertyId { get; set; }

        public int AgentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime FirstPayment { get; set; }

        public PaymentFrequency PaymentFrequency { get; set; }

        public double Deposit { get; set; }

        public virtual Property Property { get; set; }

        public virtual Agent Agent { get; set; }

        public virtual ICollection<Tenant> Tenants { get; }

        public virtual ICollection<Rate> Rates { get; }

        public virtual ICollection<AccountEntry> AccountEntries { get; }

        public virtual ICollection<ScheduledPayment> ScheduledPayments { get; }
    }
}
