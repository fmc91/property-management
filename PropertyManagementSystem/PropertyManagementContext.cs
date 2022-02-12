using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PropertyManagementCommon.Model;

namespace PropertyManagementData
{
    public class PropertyManagementContext : DbContext
    {
        public DbSet<Agent> Agent { get; set; }

        public DbSet<Broker> Broker { get; set; }

        public DbSet<Owner> Owner { get; set; }

        public DbSet<ElectricalInspectionCertificate> ElectricalInspectionCertificate { get; set; }

        public DbSet<EnergyPerformanceCertificate> EnergyPerformanceCertificate { get; set; }

        public DbSet<Expense> Expense { get; set; }

        public DbSet<Improvement> Improvement { get; set; }

        public DbSet<GasSafetyCertificate> GasSafetyCertificate { get; set; }

        public DbSet<Insurance> Insurance { get; set; }

        public DbSet<Insurer> Insurer { get; set; }

        public DbSet<Property> Property { get; set; }

        public DbSet<PurchaseCost> PurchaseCost { get; set; }

        public DbSet<Rate> Rate { get; set; }

        public DbSet<Tenancy> Tenancy { get; set; }

        public DbSet<Tenant> Tenant { get; set; }

        public DbSet<ActualPayment> ActualPayment { get; set; }

        public DbSet<ScheduledPayment> ScheduledPayment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PropertyManagement.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>()
                .HasKey(p => p.PropertyId);

            modelBuilder.Entity<Tenancy>()
                .HasKey(t => t.TenancyId);

            modelBuilder.Entity<Tenant>()
                .HasKey(t => t.TenantId);

            modelBuilder.Entity<Tenant>()
                .HasKey(t => t.TenantId);

            modelBuilder.Entity<ActualPayment>()
                .HasKey(p => p.ActualPaymentId);

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

            modelBuilder.Entity<Insurance>()
                .HasKey(i => i.InsuranceId);

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
                .HasOne<Owner>()
                .WithMany()
                .HasForeignKey(p => p.OwnerId);

            modelBuilder.Entity<Tenancy>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(t => t.PropertyId);

            modelBuilder.Entity<PurchaseCost>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(pc => pc.PropertyId);

            modelBuilder.Entity<Improvement>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(i => i.PropertyId);

            modelBuilder.Entity<Expense>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(e => e.PropertyId);

            modelBuilder.Entity<Insurance>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(i => i.PropertyId);

            modelBuilder.Entity<GasSafetyCertificate>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(gsc => gsc.PropertyId);

            modelBuilder.Entity<ElectricalInspectionCertificate>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(eic => eic.PropertyId);

            modelBuilder.Entity<EnergyPerformanceCertificate>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(epc => epc.PropertyId);

            modelBuilder.Entity<Tenant>()
                .HasOne<Tenancy>()
                .WithMany()
                .HasForeignKey(t => t.TenancyId);

            modelBuilder.Entity<Rate>()
                .HasOne<Tenancy>()
                .WithMany()
                .HasForeignKey(rp => rp.TenancyId);

            modelBuilder.Entity<Tenancy>()
                .HasOne<Agent>()
                .WithMany()
                .HasForeignKey(t => t.AgentId);

            modelBuilder.Entity<ActualPayment>()
                .HasOne<Tenancy>()
                .WithMany()
                .HasForeignKey(p => p.TenancyId);

            modelBuilder.Entity<ScheduledPayment>()
                .HasOne<Tenancy>()
                .WithMany()
                .HasForeignKey(p => p.TenancyId);

            modelBuilder.Entity<Insurance>()
                .HasOne<Broker>()
                .WithMany()
                .HasForeignKey(i => i.BrokerId);

            modelBuilder.Entity<Insurance>()
                .HasOne<Insurer>()
                .WithMany()
                .HasForeignKey(i => i.InsurerId);
        }
    }
}
