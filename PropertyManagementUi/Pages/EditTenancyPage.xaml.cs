using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PropertyManagementService;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for EditTenancyPage.xaml
    /// </summary>
    public partial class EditTenancyPage : Page, INotifyPropertyChanged
    {
        private readonly AppController _appController;

        private bool _addingAgent;

        public event PropertyChangedEventHandler PropertyChanged;

        public EditTenancyPage(AppController appController, EditTenancyViewModel viewModel)
        {
            _appController = appController;
            ViewModel = viewModel;

            InitializeComponent();
        }

        private EditTenancyViewModel ViewModel
        {
            get { return (EditTenancyViewModel)DataContext; }

            set { DataContext = value; }
        }

        public bool AddingAgent
        {
            get { return _addingAgent; }

            set
            {
                if (_addingAgent != value)
                {
                    _addingAgent = value;
                    OnPropertyChanged();
                }
            }
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            //NEED TO CHANGE VALIDATION
            if (ViewModel.Agent == null || ViewModel.Tenants.Any(t => String.IsNullOrWhiteSpace(t.Name) || String.IsNullOrWhiteSpace(t.Telephone)))
                return;

            _appController.UpdateTenancy(ViewModel);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.TenancyDetailsPage(ViewModel.TenancyId);
        }

        private void AddTenantButtonClick(object sender, RoutedEventArgs e)
        {
            var newTenant = new Tenant();

            ViewModel.Tenants.Add(newTenant);
        }

        private void RemoveTenantButtonClick(object sender, RoutedEventArgs e)
        {
            var tenant = (Tenant)((Control)sender).Tag;

            ViewModel.Tenants.Remove(tenant);
        }

        private void AddRateButtonClick(object sender, RoutedEventArgs e)
        {
            DateTime startDate = ViewModel.Rates.Count == 0 ?
                ViewModel.StartDate :
                ViewModel.Rates[^1].EndDate == ViewModel.EndDate ? ViewModel.EndDate : ViewModel.Rates[^1].EndDate.AddDays(1);

            DateTime endDate = ViewModel.EndDate;

            var newRate = new Rate
            {
                StartDate = startDate,
                EndDate = endDate
            };

            ViewModel.Rates.Add(newRate);
        }

        private void RemoveRateButtonClick(object sender, RoutedEventArgs e)
        {
            var rate = (Rate)((Control)sender).Tag;

            ViewModel.Rates.Remove(rate);
        }

        private void AddAgentButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewAgent = new Agent();
            AddingAgent = true;
        }

        private void AddAgentOkButtonClick(object sender, RoutedEventArgs e)
        {
            //NEED TO CHANGE VALIDATION
            if (String.IsNullOrWhiteSpace(ViewModel.NewAgent.Name) ||
                String.IsNullOrWhiteSpace(ViewModel.NewAgent.Telephone))
                return;

            ViewModel.Agents.Add(ViewModel.NewAgent);

            ViewModel.Agent = ViewModel.NewAgent;

            ViewModel.NewAgent = null;
            AddingAgent = false;
        }

        private void AddAgentCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewAgent = null;
            AddingAgent = false;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
