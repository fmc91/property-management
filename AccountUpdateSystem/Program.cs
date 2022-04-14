using System;
using Microsoft.EntityFrameworkCore;
using PropertyManagementCommon;
using PropertyManagementData;
using PropertyManagementData.Model;

namespace AccountUpdateSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new PropertyManagementContext(CreateDbContextOptions());

            var scheduledPayments = db.ScheduledPayment
                .Where(p => !p.Processed && p.Date <= DateTime.Today);

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
            }

            db.SaveChanges();
        }

        private static DbContextOptions<PropertyManagementContext> CreateDbContextOptions()
        {
            var builder = new DbContextOptionsBuilder<PropertyManagementContext>()
                .UseLazyLoadingProxies()
                .UseSqlite(@"Data Source=C:\Users\fazal\source\repos\PropertyManagementSystem\PropertyManagementData\PropertyManagement.db");

            return builder.Options;
        }
    }
}