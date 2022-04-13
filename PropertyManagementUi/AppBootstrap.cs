using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagementService.Model;
using PropertyManagementService.Profiles;
using Entities = PropertyManagementData.Model;
using Microsoft.EntityFrameworkCore;
using PropertyManagementData;
using PropertyManagementUi.Profiles;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi
{
    public static class AppBootstrap
    {
        public static MapperConfiguration CreateMapperConfig() => new MapperConfiguration(cfg =>
        {
            //Entity Layer <==> Domain Layer
            cfg.AddProfile<AccountEntryProfile>();
            cfg.AddProfile<ElectricalInspectionCertificateProfile>();
            cfg.AddProfile<EnergyPerformanceCertificateProfile>();
            cfg.AddProfile<ExpenseProfile>();
            cfg.AddProfile<GasSafetyCertificateProfile>();
            cfg.AddProfile<ImprovementProfile>();
            cfg.AddProfile<InsurancePolicyProfile>();
            cfg.AddProfile<PropertyProfile>();
            cfg.AddProfile<PurchaseCostProfile>();
            cfg.AddProfile<RateProfile>();
            cfg.AddProfile<TenancyProfile>();
            cfg.AddProfile<TenantProfile>();
            cfg.AddProfile<ImageProfile>();

            cfg.CreateMap<Entities.Agent, Agent>()
                .ReverseMap();
            cfg.CreateMap<Entities.Broker, Broker>()
                .ReverseMap();
            cfg.CreateMap<Entities.Insurer, Insurer>()
                .ReverseMap();
            cfg.CreateMap<Entities.Owner, Owner>()
                .ReverseMap();
            cfg.CreateMap<Entities.ScheduledPayment, ScheduledPayment>()
                .ReverseMap();

            //Domain Layer <==> UI Layer
            cfg.AddProfile<NewInsurancePolicyProfile>();
            cfg.AddProfile<AddPropertyProfile>();
            cfg.AddProfile<EditPropertyProfile>();
            cfg.AddProfile<AddTenancyProfile>();
            cfg.AddProfile<EditTenancyProfile>();
            cfg.AddProfile<PropertyDetailsProfile>();
            cfg.AddProfile<PropertySummaryProfile>();
            cfg.AddProfile<TenancyDetailsProfile>();

            cfg.CreateMap<Tenancy, TenancySummaryViewModel>();
        });

        private static DbContextOptions<PropertyManagementContext> CreateDbContextOptions()
        {
            var builder = new DbContextOptionsBuilder<PropertyManagementContext>()
                .UseLazyLoadingProxies()
                .UseSqlite(@"Data Source=C:\Users\fazal\source\repos\PropertyManagementSystem\PropertyManagementData\PropertyManagement.db");

            return builder.Options;
        }

        public static ServiceProvider GetServiceProvider()
        {
            return new ServiceProvider(CreateDbContextOptions(), CreateMapperConfig());
        }
    }
}
