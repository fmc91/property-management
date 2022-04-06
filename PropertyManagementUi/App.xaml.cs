using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using AutoMapper;
using PropertyManagementBootstrap;
using PropertyManagementService;
using PropertyManagementService.Model;
using PropertyManagementUi.Profiles;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            NavigationWindow window = new NavigationWindow
            {
                Width = 1300,
                Height = 800,
                MinWidth = 650,
                MinHeight = 400,
                ShowsNavigationUI = false
            };

            window.Navigated += (s, e) => window.NavigationService.RemoveBackEntry();

            window.Show();

            var serviceProvider = AppBootstrap.GetServiceProvider();
            var mapperConfig = CreateMapperConfig();

            var appController = new AppController(window, serviceProvider, new RateAssistant(), new Mapper(mapperConfig));

            appController.IndexPage();
        }

        private MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
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
}
