using PropertyManagementService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AppController _appController;

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

            var propertyServiceCreator = new PropertyServiceCreator();

            _appController = new AppController(window, propertyServiceCreator.Create());

            _appController.IndexPage();
        }
    }
}
