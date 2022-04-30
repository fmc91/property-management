using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using AutoMapper;
using PropertyManagementService;
using PropertyManagementService.Model;
using PropertyManagementUi.Profiles;
using PropertyManagementUi.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PropertyManagementService.Profiles;
using PropertyManagementData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PropertyManagementCommon;
using Entities = PropertyManagementData.Model;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            InitializeComponent();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host = CreateHostBuilder()
                .Build();

            _host.Start();

            _host.Services.GetRequiredService<MainWindow>().Show();
            _host.Services.GetRequiredService<AppController>().IndexPage();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            _host.StopAsync().Wait();
            _host.Dispose();
        }

        private IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddSingleton<MainWindow>()
                        .AddSingleton<NavigationDelegate>(serviceProvider => serviceProvider.GetService<MainWindow>().Navigate)
                        .AddSingleton<AppController>()
                        .AddSingleton<IConfigurationProvider>(CreateMapperConfig())
                        .AddTransient<IMapper, Mapper>()
                        .AddTransient<PaymentService>()
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

        private MapperConfiguration CreateMapperConfig() => new(cfg =>
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
