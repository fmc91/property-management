using System;
using PropertyManagementService;
using PropertyManagementService.Model;
using PropertyManagementBootstrap;
using PropertyManagementCommon;

namespace AccountUpdateSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = AppBootstrap.GetServiceProvider();

            using var propertyService = serviceProvider.GetPropertyService();

            var scheduledPayments = propertyService.GetActionableScheduledPayments();

            foreach (var p in scheduledPayments)
            {
                p.Tenancy.AccountEntries.Add(new AccountEntry
                {
                    Date = p.Date,
                    Kind = AccountEntryKind.AmountOwed,
                    Amount = p.Amount * -1,
                    Description = $"Rent payment due {p.Date:dd/MM/yyyy}"
                });

                p.Processed = true;

                propertyService.UpdateScheduledPayment(p);
            }
        }
    }
}