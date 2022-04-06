using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PropertyManagementData.Model;

namespace PropertyManagementData
{
    public class PropertyManagementContext : DbContext
    {
        public PropertyManagementContext(DbContextOptions<PropertyManagementContext> options) : base(options) { }

        public DbSet<Agent> Agent { get; set; }

        public DbSet<Broker> Broker { get; set; }

        public DbSet<Owner> Owner { get; set; }

        public DbSet<ElectricalInspectionCertificate> ElectricalInspectionCertificate { get; set; }

        public DbSet<EnergyPerformanceCertificate> EnergyPerformanceCertificate { get; set; }

        public DbSet<Expense> Expense { get; set; }

        public DbSet<Improvement> Improvement { get; set; }

        public DbSet<GasSafetyCertificate> GasSafetyCertificate { get; set; }

        public DbSet<InsurancePolicy> Insurance { get; set; }

        public DbSet<Insurer> Insurer { get; set; }

        public DbSet<Property> Property { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<PurchaseCost> PurchaseCost { get; set; }

        public DbSet<Rate> Rate { get; set; }

        public DbSet<Tenancy> Tenancy { get; set; }

        public DbSet<Tenant> Tenant { get; set; }

        public DbSet<AccountEntry> AccountEntry { get; set; }

        public DbSet<ScheduledPayment> ScheduledPayment { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder
        //        .UseLazyLoadingProxies()
        //        .UseSqlite(@"Data Source=PropertyManagement.db");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>()
                .HasKey(p => p.PropertyId);

            modelBuilder.Entity<Image>()
                .HasKey(i => i.ImageId);

            modelBuilder.Entity<Tenancy>()
                .HasKey(t => t.TenancyId);

            modelBuilder.Entity<Tenant>()
                .HasKey(t => t.TenantId);

            modelBuilder.Entity<AccountEntry>()
                .HasKey(e => e.AccountEntryId);

            modelBuilder.Entity<ScheduledPayment>()
                .HasKey(p => p.ScheduledPaymentId);

            modelBuilder.Entity<Agent>()
                .HasKey(a => a.AgentId);

            modelBuilder.Entity<Owner>()
                .HasKey(o => o.OwnerId);

            modelBuilder.Entity<Rate>()
                .HasKey(rp => rp.RateId);

            modelBuilder.Entity<PurchaseCost>()
                .HasKey(pc => pc.PurchaseCostId);

            modelBuilder.Entity<Improvement>()
                .HasKey(i => i.ImprovementId);

            modelBuilder.Entity<Expense>()
                .HasKey(e => e.ExpenseId);

            modelBuilder.Entity<InsurancePolicy>()
                .HasKey(i => i.InsurancePolicyId);

            modelBuilder.Entity<Broker>()
                .HasKey(b => b.BrokerId);

            modelBuilder.Entity<Insurer>()
                .HasKey(i => i.InsurerId);

            modelBuilder.Entity<GasSafetyCertificate>()
                .HasKey(gsc => gsc.GasCertificateId);

            modelBuilder.Entity<ElectricalInspectionCertificate>()
                .HasKey(eic => eic.ElectricalCertificateId);

            modelBuilder.Entity<EnergyPerformanceCertificate>()
                .HasKey(epc => epc.EnergyCertificateId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Image)
                .WithOne()
                .HasForeignKey<Image>(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tenancy>()
                .HasOne(t => t.Property)
                .WithMany(p => p.Tenancies)
                .HasForeignKey(t => t.PropertyId);

            modelBuilder.Entity<PurchaseCost>()
                .HasOne<Property>()
                .WithMany(p => p.PurchaseCosts)
                .HasForeignKey(pc => pc.PropertyId);

            modelBuilder.Entity<Improvement>()
                .HasOne<Property>()
                .WithMany(p => p.Improvements)
                .HasForeignKey(i => i.PropertyId);

            modelBuilder.Entity<Expense>()
                .HasOne<Property>()
                .WithMany(p => p.Expenses)
                .HasForeignKey(e => e.PropertyId);

            modelBuilder.Entity<InsurancePolicy>()
                .HasOne<Property>()
                .WithMany(p => p.InsurancePolicies)
                .HasForeignKey(i => i.PropertyId);

            modelBuilder.Entity<GasSafetyCertificate>()
                .HasOne<Property>()
                .WithMany(p => p.GasSafetyCertificates)
                .HasForeignKey(gsc => gsc.PropertyId);

            modelBuilder.Entity<ElectricalInspectionCertificate>()
                .HasOne<Property>()
                .WithMany(p => p.ElectricalInspectionCertificates)
                .HasForeignKey(eic => eic.PropertyId);

            modelBuilder.Entity<EnergyPerformanceCertificate>()
                .HasOne<Property>()
                .WithMany(p => p.EnergyPerformanceCertificates)
                .HasForeignKey(epc => epc.PropertyId);

            modelBuilder.Entity<Tenant>()
                .HasOne<Tenancy>()
                .WithMany(t => t.Tenants)
                .HasForeignKey(t => t.TenancyId);

            modelBuilder.Entity<Rate>()
                .HasOne<Tenancy>()
                .WithMany(t => t.Rates)
                .HasForeignKey(rp => rp.TenancyId);

            modelBuilder.Entity<Tenancy>()
                .HasOne(t => t.Agent)
                .WithMany()
                .HasForeignKey(t => t.AgentId);

            modelBuilder.Entity<AccountEntry>()
                .HasOne<Tenancy>()
                .WithMany(t => t.AccountEntries)
                .HasForeignKey(p => p.TenancyId);

            modelBuilder.Entity<ScheduledPayment>()
                .HasOne(p => p.Tenancy)
                .WithMany(t => t.ScheduledPayments)
                .HasForeignKey(p => p.TenancyId);

            modelBuilder.Entity<InsurancePolicy>()
                .HasOne(p => p.Broker)
                .WithMany()
                .HasForeignKey(i => i.BrokerId);

            modelBuilder.Entity<InsurancePolicy>()
                .HasOne(p => p.Insurer)
                .WithMany()
                .HasForeignKey(i => i.InsurerId);
        }
    }
}
