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
using PropertyManagementCommon;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for TenanacyDetailsPage.xaml
    /// </summary>
    public partial class TenancyDetailsPage : Page, INotifyPropertyChanged
    {
        private AppController _appController;
        private bool _addingPayment;

        public event PropertyChangedEventHandler PropertyChanged;

        public TenancyDetailsPage(AppController appController, TenancyDetailsViewModel viewModel)
        {
            _appController = appController;
            ViewModel = viewModel;
            InitializeComponent();
        }

        public TenancyDetailsViewModel ViewModel
        {
            get { return (TenancyDetailsViewModel)DataContext; }
            
            set { DataContext = value; }
        }

        public bool AddingPayment
        {
            get { return _addingPayment; }

            set
            {
                if (_addingPayment != value)
                {
                    _addingPayment = value;
                    OnPropertyChanged();
                }
            }
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.EditTenancyPage(ViewModel.TenancyId);
        }

        private void BackLinkClick(object sender, RoutedEventArgs e)
        {
            _appController.PropertyDetailsPage(ViewModel.PropertyId);
        }

        private void AddPaymentLinkClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewPayment = new AccountEntry
            {
                Date = DateTime.Today,
                Kind = AccountEntryKind.RealisedPayment
            };

            AddingPayment = true;
        }

        private void ConfirmAddPaymentButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.AddPayment(ViewModel.TenancyId, ViewModel.NewPayment);
            ViewModel.AccountEntries.Add(ViewModel.NewPayment);

            ViewModel.NewPayment = null;
            AddingPayment = false;
        }

        private void CancelAddPaymentButtonClick(object sender, RoutedEventArgs e)
        {
            AddingPayment = false;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
