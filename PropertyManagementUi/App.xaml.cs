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

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var window = new NavigationWindow
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

            var appController = new AppController(window, serviceProvider, new RateAssistant(), new Mapper(AppBootstrap.CreateMapperConfig()));

            appController.IndexPage();
        }
    }
}
