using PropertyManagementCommon;
using PropertyManagementCommon.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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

        private List<Expense> _editedExpenses;

        private List<Improvement> _editedImprovements;

        private List<Expense> _deletedExpenses;

        private List<Improvement> _deletedImprovements;

        private Dictionary<string, List<string>> _propertyDependencies = new Dictionary<string, List<string>>
        {
            { nameof(ShowNoInsuranceText), new List<string> { nameof(ViewModel), nameof(AddingInsurance) } },
            { nameof(ShowInsuranceDetails), new List<string> { nameof(ViewModel), nameof(AddingInsurance) } },
            { nameof(ShowNoElectricalCertificateText), new List<string> { nameof(ViewModel), nameof(AddingElectricalCertificate) } },
            { nameof(ShowElectricalCertificateDetails), new List<string> { nameof(ViewModel), nameof(AddingElectricalCertificate) } },
            { nameof(ShowNoGasCertificateText), new List<string> { nameof(ViewModel), nameof(AddingGasCertificate) } },
            { nameof(ShowGasCertificateDetails), new List<string> { nameof(ViewModel), nameof(AddingGasCertificate) } },
            { nameof(ShowNoEnergyCertificateText), new List<string> { nameof(ViewModel), nameof(AddingEnergyCertificate) } },
            { nameof(ShowEnergyCertificateDetails), new List<string> { nameof(ViewModel), nameof(AddingEnergyCertificate) } },
            { nameof(AddingOrEditingExpenses), new List<string> { nameof(AddingExpense), nameof(EditingExpenses) } },
            { nameof(AddingOrEditingImprovements), new List<string> { nameof(AddingImprovement), nameof(EditingImprovements) } }
        };

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

        public bool ShowNoInsuranceText => ViewModel == null ? false : ViewModel.Insurance == null && !AddingInsurance;

        public bool ShowInsuranceDetails => ViewModel == null ? false : ViewModel.Insurance != null && !AddingInsurance;

        public bool ShowNoElectricalCertificateText => ViewModel == null ? false : ViewModel.ElectricalInspectionCertificate == null && !AddingElectricalCertificate;

        public bool ShowElectricalCertificateDetails => ViewModel == null ? false : ViewModel.ElectricalInspectionCertificate != null && !AddingElectricalCertificate;

        public bool ShowNoGasCertificateText => ViewModel == null ? false : ViewModel.GasSafetyCertificate == null && !AddingGasCertificate;

        public bool ShowGasCertificateDetails => ViewModel == null ? false : ViewModel.GasSafetyCertificate != null && !AddingGasCertificate;

        public bool ShowNoEnergyCertificateText => ViewModel == null ? false : ViewModel.EnergyPerformanceCertificate == null && !AddingEnergyCertificate;

        public bool ShowEnergyCertificateDetails => ViewModel == null ? false : ViewModel.EnergyPerformanceCertificate != null && !AddingEnergyCertificate;

        public bool AddingOrEditingExpenses => AddingExpense || EditingExpenses;

        public bool AddingOrEditingImprovements => AddingImprovement || EditingImprovements;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            UpdateDependentProperties(propertyName);
        }

        private void UpdateDependentProperties(string propertyName)
        {
            foreach (var (k, v) in _propertyDependencies)
            {
                if (v.Contains(propertyName))
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(k));
            }
        }

        private void BackLinkClick(object sender, RoutedEventArgs e)
        {
            _appController.IndexPage();
        }

        private void TenancyDetailsButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.TenancyDetailsPage(ViewModel.CurrentTenancy.TenancyId);
        }

        private void AddTenancyButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.AddTenancyPage(ViewModel.PropertyId);
        }

        private void AddInsuranceButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewInsurance = new NewInsuranceViewModel
            {
                PropertyId = ViewModel.PropertyId,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                Insurers = _appController.GetAllInsurers().ToList(),
                Brokers = _appController.GetAllBrokers().ToList()
            };

            AddingInsurance = true;
        }

        private void AddInsuranceOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurance;

            //NEED TO CHANGE VALIDATION
            if (newInsurance.Broker == null ||
                newInsurance.Insurer == null ||
                newInsurance.EndDate < newInsurance.StartDate)
                return;

            _appController.AddInsurance(newInsurance);

            ViewModel.Insurance = _appController.GetLatestInsurance(ViewModel.PropertyId);

            ViewModel.NewInsurance = null;

            AddingInsurance = false;
        }

        private void AddInsuranceCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewInsurance = null;

            AddingInsurance = false;
        }

        private void AddInsurerButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurance;

            newInsurance.NewInsurer = new Insurer();

            AddingInsurer = true;
        }

        private void AddInsurerOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurance;
            var newInsurer = newInsurance.NewInsurer;

            //NEED TO CHANGE VALIDATION
            if (String.IsNullOrWhiteSpace(newInsurer.Name) ||
                String.IsNullOrWhiteSpace(newInsurer.Telephone))
                return;

            _appController.AddInsurer(newInsurer);
            newInsurance.Insurers.Add(newInsurer);

            newInsurance.Insurer = newInsurer;

            newInsurance.NewInsurer = null;

            AddingInsurer = false;
        }

        private void AddInsurerCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewInsurance.NewInsurer = null;
            AddingInsurer = false;
        }

        private void AddBrokerButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurance;

            newInsurance.NewBroker = new Broker();

            AddingBroker = true;
        }

        private void AddBrokerOkButtonClick(object sender, RoutedEventArgs e)
        {
            var newInsurance = ViewModel.NewInsurance;
            var newBroker = newInsurance.NewBroker;

            //NEED TO CHANGE VALIDATION
            if (String.IsNullOrWhiteSpace(newBroker.Name) ||
                String.IsNullOrWhiteSpace(newBroker.Telephone))
                return;

            _appController.AddBroker(newBroker);
            newInsurance.Brokers.Add(newBroker);

            newInsurance.Broker = newBroker;

            newInsurance.NewBroker = null;

            AddingBroker = false;
        }

        private void AddBrokerCancelButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewInsurance.NewBroker = null;
            AddingBroker = false;
        }

        private void AddElectricalCertificateButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.NewElectricalInspectionCertificate = new ElectricalInspectionCertificate
            {
                PropertyId = ViewModel.PropertyId,
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

            _appController.AddElectricalInspectionCertificate(newCertificate);

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
                PropertyId = ViewModel.PropertyId,
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

            _appController.AddGasSafetyCertificate(newCertificate);

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
                PropertyId = ViewModel.PropertyId,
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

            _appController.AddEnergyPerformanceCertificate(newCertificate);

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
            ViewModel.NewExpense = new Expense { PropertyId = ViewModel.PropertyId, Date = DateTime.Today };
            AddingExpense = true;
        }

        private void AddExpenseOkButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.AddExpense(ViewModel.NewExpense);
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
            ViewModel.NewImprovement = new Improvement { PropertyId = ViewModel.PropertyId, Date = DateTime.Today };
            AddingImprovement = true;
        }

        private void AddImprovementOkButtonClick(object sender, RoutedEventArgs e)
        {
            _appController.AddImprovement(ViewModel.NewImprovement);
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
            ViewModel.ImprovementsForEdit = new ObservableCollection<Improvement>(
                ViewModel.Improvements.Select(i => new Improvement
                {
                    ImprovementId = i.ImprovementId,
                    PropertyId = i.PropertyId,
                    Date = i.Date,
                    Description = i.Description,
                    Cost = i.Cost
                }));

            _editedImprovements = new List<Improvement>();
            _deletedImprovements = new List<Improvement>();

            EditingImprovements = true;
        }

        private void EditExpensesButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ExpensesForEdit = new ObservableCollection<Expense>(
                ViewModel.Expenses.Select(i => new Expense
                {
                    ExpenseId  = i.ExpenseId,
                    PropertyId = i.PropertyId,
                    Date = i.Date,
                    Description = i.Description,
                    Amount = i.Amount
                }));

            _editedExpenses = new List<Expense>();
            _deletedExpenses = new List<Expense>();

            EditingExpenses = true;
        }

        private void EditImprovementsOkButtonClick(object sender, RoutedEventArgs e)
        {
            _editedImprovements.RemoveAll(x => _deletedImprovements.Contains(x));

            if (_editedImprovements.Count != 0)
                _appController.EditImprovements(_editedImprovements);

            if (_deletedImprovements.Count != 0)
                _appController.DeleteImprovements(_deletedImprovements);

            ViewModel.Improvements = ViewModel.ImprovementsForEdit;
            EditingImprovements = false;
            ViewModel.ImprovementsForEdit = null;

            _editedImprovements = null;
            _deletedImprovements = null;
        }

        private void EditImprovementsCancelButtonClick(object sender, RoutedEventArgs e)
        {
            EditingImprovements = false;
            ViewModel.ImprovementsForEdit = null;

            _editedImprovements = null;
            _deletedImprovements = null;
        }

        private void EditExpensesOkButtonClick(object sender, RoutedEventArgs e)
        {
            _editedExpenses.RemoveAll(x => _deletedExpenses.Contains(x));

            if (_editedExpenses.Count != 0)
                _appController.EditExpenses(_editedExpenses);

            if (_deletedExpenses.Count != 0)
                _appController.DeleteExpenses(_deletedExpenses);

            ViewModel.Expenses = ViewModel.ExpensesForEdit;
            EditingExpenses = false;
            ViewModel.ExpensesForEdit = null;

            _editedExpenses = null;
            _deletedExpenses = null;
        }

        private void EditExpensesCancelButtonClick(object sender, RoutedEventArgs e)
        {
            EditingExpenses = false;
            ViewModel.ExpensesForEdit = null;

            _editedExpenses = null;
            _deletedExpenses = null;
        }

        private void DeleteExpenseButtonClick(object sender, RoutedEventArgs e)
        {
            var expense = (Expense)((Control)sender).Tag;

            ViewModel.ExpensesForEdit.Remove(expense);
            _editedExpenses.Remove(expense);

            _deletedExpenses.Add(expense);
        }

        private void DeleteImprovementButtonClick(object sender, RoutedEventArgs e)
        {
            var improvement = (Improvement)((Control)sender).Tag;

            ViewModel.ImprovementsForEdit.Remove(improvement);
            _editedImprovements.Remove(improvement);

            _deletedImprovements.Add(improvement);
        }

        private void ImprovementDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var improvement = (Improvement)((Control)sender).Tag;
            _editedImprovements.Add(improvement);
        }

        private void ImprovementDescriptionChanged(object sender, TextChangedEventArgs e)
        {
            var improvement = (Improvement)((Control)sender).Tag;
            _editedImprovements.Add(improvement);
        }

        private void ImprovementCostChanged(object sender, TextChangedEventArgs e)
        {
            var improvement = (Improvement)((Control)sender).Tag;
            _editedImprovements.Add(improvement);
        }

        private void ExpenseDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var expense = (Expense)((Control)sender).Tag;
            _editedExpenses.Add(expense);
        }

        private void ExpenseDescriptionChanged(object sender, TextChangedEventArgs e)
        {
            var expense = (Expense)((Control)sender).Tag;
            _editedExpenses.Add(expense);
        }

        private void ExpenseAmountChanged(object sender, TextChangedEventArgs e)
        {
            var expense = (Expense)((Control)sender).Tag;
            _editedExpenses.Add(expense);
        }
    }
}
