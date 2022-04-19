using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementService.Profiles;
using PropertyManagementUi.Profiles;
using PropertyManagementUi.ViewModels;
using PropertyManagementData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PropertyManagementService;
using PropertyManagementCommon;
using Entities = PropertyManagementData.Model;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PropertyManagementUi
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder()
                .Build();

            host.RunApplication();
        }

        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<App>()
                        .AddSingleton<AppController>()
                        .AddSingleton<IConfigurationProvider>(CreateMapperConfig())
                        .AddTransient<IMapper, Mapper>()
                        .AddTransient<RateAssistant>()
                        .AddTransient<PropertyService>()
                        .AddTransient<ImageService>()
                        .AddDbContextFactory<PropertyManagementContext>(builder =>
                        {
                            builder.UseLazyLoadingProxies()
                                .UseSqlite(context.Configuration.GetConnectionString("PropertyDatabase"));
                        });

                    services.Configure<StorageOptions>(context.Configuration.GetSection(StorageOptions.SectionName));
                });
        }

        private static MapperConfiguration CreateMapperConfig() => new(cfg =>
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
    }
}
