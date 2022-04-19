using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi
{
    /// <summary>
    /// Interaction logic for PropertyDetailsPage.xaml
    /// </summary>
    public partial class PropertyDetailsPage : Page, INotifyPropertyChanged
    {
        private AppController _appController;

        private bool _addingInsurance;

        private bool _addingElectricalCertificate;

        private bool _addingGasCertificate;

        private bool _addingEnergyCertificate;

        private bool _addingInsurer;

        private bool _addingBroker;

        private bool _addingImprovement;

        private bool _addingExpense;

        private bool _editingExpenses;

        private bool _editingImprovements;

        public PropertyDetailsPage(AppController appController, PropertyDetailsViewModel viewModel)
        {
            _appController = appController;
            ViewModel = viewModel;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private PropertyDetailsViewModel ViewModel
        {
            get { return (PropertyDetailsViewModel)DataContext; }

            set
            {
                DataContext = value;
                value.PropertyChanged += (s, e) => OnPropertyChanged();
                OnPropertyChanged(nameof(ViewModel));
            }
        }

        public bool AddingInsurance
        {
            get { return _addingInsurance; }

            set
            {
                if (_addingInsurance != value)
                {
                    _addingInsurance = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AddingElectricalCertificate
        {
            get { return _addingElectricalCertificate; }

            set
            {
                if (_addingElectricalCertificate != value)
                {
                    _addingElectricalCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AddingGasCertificate
        {
            get { return _addingGasCertificate; }

            set
            {
                if (_addingGasCertificate != value)
                {
                    _addingGasCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AddingEnergyCertificate
        {
            get { return _addingEnergyCertificate; }

            set
            {
                if (_addingEnergyCertificate != value)
                {
                    _addingEnergyCertificate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AddingInsurer
        {
            get { return _addingInsurer; }

            set
            {
                if (_addingInsurer != value)
                {
                    _addingInsurer = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AddingBroker
        {
            get { return _addingBroker; }

            set
            {
                if (_addingBroker != value)
                {
                    _addingBroker = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AddingExpense
        {
            get { return _addingExpense; }

            set
            {
                if (_addingExpense != value)
                {
                    _addingExpense = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AddingImprovement
        {
            get { return _addingImprovement; }

            set
            {
                if (_addingImprovement != value)
                {
                    _addingImprovement = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EditingExpenses
        {
            get { return _editingExpenses; }

            set
            {
                if (_editingExpenses != value)
                {
                    _editingExpenses = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EditingImprovements
        {
            get { return _editingImprovements; }

            set
            {
                if (_editingImprovements != value)
                {
                    _editingImprovements = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.EditPropertyPage(ViewModel.PropertyId);
        }

        private void BackLinkClick(object sender, RoutedEventArgs e)
        {
            _appController.IndexPage();
        }

        private void TenancyDetailsButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.TenancyDetailsPage(ViewModel.Tenancy.TenancyId);
        }

        private void AddTenancyButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.AddTenancyPage(ViewModel.PropertyId);
        }

        private void AddInsurancePolicyButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurancePolicy = _appController.GetNewInsurancePolicyViewModel(ViewModel.PropertyId);
            newInsurancePolicy.StartDate = DateTime.Today;
            newInsurancePolicy.EndDate = DateTime.Today;

            ViewModel.NewInsurancePolicy = newInsurancePolicy;

            AddingInsurance = true;
        }

        private void AddInsurancePolicyOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurancePolicy;

            //NEED TO CHANGE VALIDATION
            if (newInsurance.Broker == null ||
                newInsurance.Insurer == null ||
                newInsurance.EndDate < newInsurance.StartDate)
                return;

            _appController.AddInsurancePolicy(newInsurance);

            ViewModel.InsurancePolicy = _appController.GetLatestInsurancePolicy(ViewModel.PropertyId);

            ViewModel.NewInsurancePolicy = null;

            AddingInsurance = false;
        }

        private void AddInsurancePolicyCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewInsurancePolicy = null;

            AddingInsurance = false;
        }

        private void AddInsurerButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurancePolicy;

            newInsurance.NewInsurer = new Insurer();

            AddingInsurer = true;
        }

        private void AddInsurerOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurancePolicy;
            var newInsurer = newInsurance.NewInsurer;

            //NEED TO CHANGE VALIDATION
            if (String.IsNullOrWhiteSpace(newInsurer.Name) ||
                String.IsNullOrWhiteSpace(newInsurer.Telephone))
                return;

            newInsurance.Insurers.Add(newInsurer);

            newInsurance.Insurer = newInsurer;

            newInsurance.NewInsurer = null;

            AddingInsurer = false;
        }

        private void AddInsurerCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewInsurancePolicy.NewInsurer = null;
            AddingInsurer = false;
        }

        private void AddBrokerButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurancePolicy;

            newInsurance.NewBroker = new Broker();

            AddingBroker = true;
        }

        private void AddBrokerOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurancePolicy;
            var newBroker = newInsurance.NewBroker;

            //NEED TO CHANGE VALIDATION
            if (String.IsNullOrWhiteSpace(newBroker.Name) ||
                String.IsNullOrWhiteSpace(newBroker.Telephone))
                return;

            newInsurance.Brokers.Add(newBroker);

            newInsurance.Broker = newBroker;

            newInsurance.NewBroker = null;

            AddingBroker = false;
        }

        private void AddBrokerCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewInsurancePolicy.NewBroker = null;
            AddingBroker = false;
        }

        private void AddElectricalCertificateButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewElectricalInspectionCertificate = new ElectricalInspectionCertificate
            {
                IssueDate = DateTime.Today,
                ExpiryDate = DateTime.Today
            };

            AddingElectricalCertificate = true;
        }

        private void AddElectricalCertificateOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newCertificate = ViewModel.NewElectricalInspectionCertificate;

            //NEED TO CHANGE VALIDATION
            if (newCertificate.ExpiryDate < newCertificate.IssueDate) return;

            _appController.AddElectricalInspectionCertificate(ViewModel.PropertyId, newCertificate);

            ViewModel.ElectricalInspectionCertificate = _appController.GetLatestElectricalInspectionCertificate(ViewModel.PropertyId);

            ViewModel.NewElectricalInspectionCertificate = null;
            AddingElectricalCertificate = false;
        }

        private void AddElectricalCertificateCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewElectricalInspectionCertificate = null;
            AddingElectricalCertificate = false;
        }

        private void AddGasCertificateButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGasSafetyCertificate = new GasSafetyCertificate
            {
                IssueDate = DateTime.Today,
                ExpiryDate = DateTime.Today
            };

            AddingGasCertificate = true;
        }

        private void AddGasCertificateOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newCertificate = ViewModel.NewGasSafetyCertificate;

            //NEED TO CHANGE VALIDATION
            if (newCertificate.ExpiryDate < newCertificate.IssueDate) return;

            _appController.AddGasSafetyCertificate(ViewModel.PropertyId, newCertificate);

            ViewModel.GasSafetyCertificate = _appController.GetLatestGasSafetyCertificate(ViewModel.PropertyId);

            ViewModel.NewGasSafetyCertificate = null;
            AddingGasCertificate = false;
        }

        private void AddGasCertificateCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewGasSafetyCertificate = null;
            AddingGasCertificate = false;
        }

        private void AddEnergyCertificateButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewEnergyPerformanceCertificate = new EnergyPerformanceCertificate
            {
                IssueDate = DateTime.Today,
                ExpiryDate = DateTime.Today
            };

            AddingEnergyCertificate = true;
        }

        private void AddEnergyCertificateOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newCertificate = ViewModel.NewEnergyPerformanceCertificate;

            //NEED TO CHANGE VALIDATION
            if (newCertificate.ExpiryDate < newCertificate.IssueDate) return;

            _appController.AddEnergyPerformanceCertificate(ViewModel.PropertyId, newCertificate);

            ViewModel.EnergyPerformanceCertificate = _appController.GetLatestEnergyPerformanceCertificate(ViewModel.PropertyId);

            ViewModel.NewEnergyPerformanceCertificate = null;
            AddingEnergyCertificate = false;
        }

        private void AddEnergyCertificateCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewEnergyPerformanceCertificate = null;
            AddingEnergyCertificate = false;
        }

        private void AddExpenseButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewExpense = new Expense { Date = DateTime.Today };
            AddingExpense = true;
        }

        private void AddExpenseOkButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(ViewModel.NewExpense.Description)) return;

            _appController.AddExpense(ViewModel.PropertyId, ViewModel.NewExpense);
            ViewModel.Expenses.Add(ViewModel.NewExpense);

            ViewModel.NewExpense = null;
            AddingExpense = false;
        }

        private void AddExpenseCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewExpense = null;
            AddingExpense = false;
        }

        private void AddImprovementButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewImprovement = new Improvement { Date = DateTime.Today };
            AddingImprovement = true;
        }

        private void AddImprovementOkButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ViewModel.NewImprovement.Description)) return;

            _appController.AddImprovement(ViewModel.PropertyId, ViewModel.NewImprovement);
            ViewModel.Improvements.Add(ViewModel.NewImprovement);

            ViewModel.NewImprovement = null;
            AddingImprovement = false;
        }

        private void AddImprovementCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewImprovement = null;
            AddingImprovement = false;
        }

        private void EditImprovementsButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ImprovementsForEdit = new ObservableCollection<Improvement>(ViewModel.Improvements.Select(i => new Improvement
            {
                ImprovementId = i.ImprovementId,
                Date = i.Date,
                Description = i.Description,
                Cost = i.Cost
            }));

            EditingImprovements = true;
        }

        private void EditExpensesButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ExpensesForEdit = new ObservableCollection<Expense>(ViewModel.Expenses.Select(i => new Expense
            {
                ExpenseId  = i.ExpenseId,
                Date = i.Date,
                Description = i.Description,
                Amount = i.Amount
            }));

            EditingExpenses = true;
        }

        private void EditImprovementsOkButtonClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ImprovementsForEdit.Any(i => String.IsNullOrWhiteSpace(i.Description))) return;

            ViewModel.Improvements = ViewModel.ImprovementsForEdit;
            ViewModel.ImprovementsForEdit = null;

            _appController.UpdateImprovements(ViewModel.PropertyId, ViewModel.Improvements);

            EditingImprovements = false;
        }

        private void EditImprovementsCancelButtonClick(object sender, RoutedEventArgs e)
        {
            EditingImprovements = false;
            ViewModel.ImprovementsForEdit = null;
        }

        private void EditExpensesOkButtonClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ExpensesForEdit.Any(e => String.IsNullOrWhiteSpace(e.Description))) return;

            ViewModel.Expenses = ViewModel.ExpensesForEdit;
            ViewModel.ExpensesForEdit = null;

            _appController.UpdateExpenses(ViewModel.PropertyId, ViewModel.Expenses);

            EditingExpenses = false;
        }

        private void EditExpensesCancelButtonClick(object sender, RoutedEventArgs e)
        {
            EditingExpenses = false;
            ViewModel.ExpensesForEdit = null;
        }

        private void DeleteExpenseButtonClick(object sender, RoutedEventArgs e)
        {
            var expense = (Expense)((Control)sender).Tag;

            ViewModel.ExpensesForEdit.Remove(expense);
        }

        private void DeleteImprovementButtonClick(object sender, RoutedEventArgs e)
        {
            var improvement = (Improvement)((Control)sender).Tag;

            ViewModel.ImprovementsForEdit.Remove(improvement);
        }
    }
}
