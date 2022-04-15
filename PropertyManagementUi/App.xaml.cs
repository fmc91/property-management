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

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly AppController _appController;

        public App()
        {
            //ONLY EXISTS SO THE AUTO-GENERATED MAIN METHOD (NOT USED) DOES NOT CAUSE A COMPILE-TIME ERROR
            throw new NotImplementedException();
        }

        public App(AppController appController)
        {
            _appController = appController;
            InitializeComponent();
        }

        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var window = new MainWindow
            {
                Width = 1300,
                Height = 800,
                MinWidth = 650,
                MinHeight = 400
            };

            window.Show();

            _appController.Navigate = page => window.Navigate(page);

            _appController.IndexPage();
        }
    }
}
