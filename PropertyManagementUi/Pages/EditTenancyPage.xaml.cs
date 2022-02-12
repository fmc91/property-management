using PropertyManagementCommon.Model;
using PropertyManagementService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for EditTenancyPage.xaml
    /// </summary>
    public partial class EditTenancyPage : Page, INotifyPropertyChanged
    {
        private AppController _appController;
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
            _appController.EditTenancy(ViewModel);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.TenancyDetailsPage(ViewModel.TenancyId);
        }

        private void AddTenantButtonClick(object sender, RoutedEventArgs e)
        {
            var newTenant = new Tenant { TenancyId = ViewModel.TenancyId };

            ViewModel.Tenants.Add(newTenant);

            ViewModel.AddedTenants.Add(newTenant);
        }

        private void RemoveTenantButtonClick(object sender, RoutedEventArgs e)
        {
            var tenant = (Tenant)((Control)sender).Tag;

            ViewModel.Tenants.Remove(tenant);

            if (ViewModel.AddedTenants.Contains(tenant))
            {
                ViewModel.AddedTenants.Remove(tenant);
            }
            else
            {
                if (!ViewModel.DeletedTenants.Contains(tenant))
                {
                    if (ViewModel.ModifiedTenants.Contains(tenant))
                        ViewModel.ModifiedTenants.Remove(tenant);

                    ViewModel.DeletedTenants.Add(tenant);
                }
            }
        }

        private void AddRateButtonClick(object sender, RoutedEventArgs e)
        {
            DateTime startDate = ViewModel.Rates.Count == 0 ?
                ViewModel.StartDate :
                ViewModel.Rates[^1].EndDate == ViewModel.EndDate ? ViewModel.EndDate : ViewModel.Rates[^1].EndDate.AddDays(1);

            DateTime endDate = ViewModel.EndDate;

            var newRate = new Rate
            {
                TenancyId = ViewModel.TenancyId,
                StartDate = startDate,
                EndDate = endDate
            };

            ViewModel.Rates.Add(newRate);

            ViewModel.AddedRates.Add(newRate);
        }

        private void RemoveRateButtonClick(object sender, RoutedEventArgs e)
        {
            var rate = (Rate)((Control)sender).Tag;

            ViewModel.Rates.Remove(rate);

            if (ViewModel.AddedRates.Contains(rate))
            {
                ViewModel.AddedRates.Remove(rate);
            }
            else
            {
                if (!ViewModel.DeletedRates.Contains(rate))
                {
                    if (ViewModel.ModifiedRates.Contains(rate))
                        ViewModel.ModifiedRates.Remove(rate);

                    ViewModel.DeletedRates.Add(rate);
                }
            }
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

            _appController.AddAgent(ViewModel.NewAgent);
            ViewModel.Agents.Add(ViewModel.NewAgent);

            //ViewModel.Agent = ViewModel.NewAgent;
            ViewModel.AgentId = ViewModel.NewAgent.AgentId;

            ViewModel.NewAgent = null;
            AddingAgent = false;

            //_editTenancyDto.AgentId = ViewModel.Agent.AgentId;
        }

        private void AddAgentCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewAgent = null;
            AddingAgent = false;
        }

        private void TenantDetailsChanged(object sender, TextChangedEventArgs e)
        {
            var tenant = (Tenant)((Control)sender).Tag;

            if (tenant.TenantId != 0 && !ViewModel.ModifiedTenants.Contains(tenant))
                ViewModel.ModifiedTenants.Add(tenant);
        }

        private void RateDateChanged(object sender, RoutedEventArgs e)
        {
            var rate = (Rate)((Control)sender).Tag;

            if (rate.RateId != 0 && !ViewModel.ModifiedRates.Contains(rate))
                ViewModel.ModifiedRates.Add(rate);
        }

        private void RateAmountChanged(object sender, TextChangedEventArgs e)
        {
            var rate = (Rate)((Control)sender).Tag;

            if (rate.RateId != 0 && !ViewModel.ModifiedRates.Contains(rate))
                ViewModel.ModifiedRates.Add(rate);
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
