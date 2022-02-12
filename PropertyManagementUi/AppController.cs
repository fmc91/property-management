using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using PropertyManagementCommon.Model;
using PropertyManagementService;

namespace PropertyManagementUi
{
    public class AppController
    {
        private NavigationWindow _mainWindow;

        private PropertyService _propertyService;

        private RentAssistant _rentAssistant;

        private RateAssistant _rateAssistant;

        public AppController(NavigationWindow mainWindow, PropertyService propertyService)
        {
            _mainWindow = mainWindow;
            _propertyService = propertyService;

            _rentAssistant = new RentAssistant(propertyService);
            _rateAssistant = new RateAssistant();
        }

        public void IndexPage()
        {
            var propertySummaries = _propertyService.GetAllProperties()
                .Select(p => CreatePropertySummaryViewModel(p));

            var viewModel = new IndexViewModel { Properties = new ObservableCollection<PropertySummaryViewModel>(propertySummaries) };

            _mainWindow.Navigate(new IndexPage(this, viewModel));
        }

        public void AddPropertyPage()
        {
            var viewModel = CreateAddPropertyViewModel();
            _mainWindow.Navigate(new AddPropertyPage(this, viewModel));
        }

        public void PropertyDetailsPage(int propertyId)
        {
            var viewModel = CreatePropertyDetailsViewModel(propertyId);
            _mainWindow.Navigate(new PropertyDetailsPage(this, viewModel));
        }

        public void AddTenancyPage(int propertyId)
        {
            var viewModel = CreateAddTenancyViewModel(propertyId);
            _mainWindow.Navigate(new AddTenancyPage(this, viewModel));
        }

        public void EditTenancy(EditTenancyViewModel editTenancyViewModel)
        {
            var tenancy = _propertyService.GetTenancy(editTenancyViewModel.TenancyId);

            var prevDepositAmount = tenancy.Deposit;
            var prevFirstPayment = tenancy.FirstPayment;
            var prevPaymentFrequency = tenancy.PaymentFrequency;

            tenancy.AgentId = editTenancyViewModel.AgentId;
            tenancy.StartDate = editTenancyViewModel.StartDate;
            tenancy.EndDate = editTenancyViewModel.EndDate;
            tenancy.PaymentFrequency = editTenancyViewModel.PaymentFrequency;
            tenancy.FirstPayment = editTenancyViewModel.FirstPayment;
            tenancy.Deposit = editTenancyViewModel.DepositAmount;

            _propertyService.EditTenancy(tenancy);

            _propertyService.AddTenants(editTenancyViewModel.AddedTenants);
            _propertyService.DeleteTenants(editTenancyViewModel.DeletedTenants);
            _propertyService.EditTenants(editTenancyViewModel.ModifiedTenants);

            bool ratesModified = editTenancyViewModel.AddedRates.Count != 0 || editTenancyViewModel.DeletedRates.Count != 0 || editTenancyViewModel.ModifiedRates.Count != 0;
            bool paymentSchedulingModified = tenancy.PaymentFrequency != prevPaymentFrequency || tenancy.FirstPayment != prevFirstPayment;

            if (ratesModified)
            {
                _propertyService.AddRates(editTenancyViewModel.AddedRates);
                _propertyService.DeleteRates(editTenancyViewModel.DeletedRates);
                _propertyService.EditRates(editTenancyViewModel.ModifiedRates);
            }

            if (ratesModified || paymentSchedulingModified)
            {
                _propertyService.DeleteAllScheduledPayments(tenancy.TenancyId);

                var rates = _propertyService.GetRates(tenancy.TenancyId);

                var scheduledPayments = _rateAssistant.CreateScheduledPayments(tenancy.TenancyId, rates, tenancy.FirstPayment, tenancy.PaymentFrequency);

                _propertyService.AddScheduledPayments(scheduledPayments);
                _propertyService.AddScheduledPayment(new ScheduledPayment
                {
                    TenancyId = tenancy.TenancyId,
                    Date = tenancy.StartDate,
                    Amount = tenancy.Deposit
                });
            }
            else if (tenancy.Deposit != prevDepositAmount)
            {
                //THIS NEEDS TO CHANGE!!!
                _propertyService.AddScheduledPayment(new ScheduledPayment
                {
                    TenancyId = tenancy.TenancyId,
                    Date = tenancy.StartDate,
                    Amount = tenancy.Deposit - prevDepositAmount
                });
            }

            TenancyDetailsPage(tenancy.TenancyId);
        }

        public void EditTenancyPage(int tenancyId)
        {
            var viewModel = CreateEditTenancyViewModel(tenancyId);
            _mainWindow.Navigate(new EditTenancyPage(this, viewModel));
        }

        public void TenancyDetailsPage(int tenancyId)
        {
            var viewModel = CreateTenancyDetailsViewModel(tenancyId);
            _mainWindow.Navigate(new TenancyDetailsPage(this, viewModel));
        }

        public void AddTenancy(AddTenancyViewModel viewModel)
        {
            var newTenancy = new Tenancy
            {
                PropertyId = viewModel.PropertyId,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                FirstPayment = viewModel.FirstPayment,
                PaymentFrequency = viewModel.PaymentFrequency,
                Deposit = viewModel.DepositAmount,
                AgentId = viewModel.Agent.AgentId
            };

            _propertyService.AddTenancy(newTenancy);

            foreach (var tenant in viewModel.Tenants)
                tenant.TenancyId = newTenancy.TenancyId;

            _propertyService.AddTenants(viewModel.Tenants);

            foreach (var rate in viewModel.Rates)
                rate.TenancyId = newTenancy.TenancyId;

            _propertyService.AddRates(viewModel.Rates);

            var scheduledPayments = _rateAssistant.CreateScheduledPayments(newTenancy.TenancyId, viewModel.Rates, viewModel.FirstPayment, viewModel.PaymentFrequency);

            _propertyService.AddScheduledPayments(scheduledPayments);
            _propertyService.AddScheduledPayment(new ScheduledPayment
            {
                TenancyId = newTenancy.TenancyId,
                Date = newTenancy.StartDate,
                Amount = newTenancy.Deposit
            });

            PropertyDetailsPage(viewModel.PropertyId);
        }

        public void AddProperty(AddPropertyViewModel viewModel)
        {
            var newProperty = new Property
            {
                PropertyType = viewModel.PropertyType,
                OwnerId = viewModel.Owner.OwnerId,
                StreetAddress1 = viewModel.StreetAddress1,
                StreetAddress2 = viewModel.StreetAddress2,
                Locality = viewModel.Locality,
                City = viewModel.City,
                County = viewModel.County,
                Country = viewModel.Country,
                Postcode = viewModel.Postcode,
                PurchaseDate = viewModel.PurchaseDate,
                PurchasePrice = viewModel.PurchasePrice
            };

            _propertyService.AddProperty(newProperty);

            foreach (var pc in viewModel.PurchaseCosts)
                pc.PropertyId = newProperty.PropertyId;
            
            _propertyService.AddPurchaseCosts(viewModel.PurchaseCosts);

            IndexPage();
        }

        public IEnumerable<Insurer> GetAllInsurers()
        {
            return _propertyService.GetAllInsurers();
        }

        public void AddAgent(Agent agent)
        {
            _propertyService.AddAgent(agent);
        }

        public IEnumerable<Broker> GetAllBrokers()
        {
            return _propertyService.GetAllBrokers();
        }

        public void AddOwner(Owner owner)
        {
            _propertyService.AddOwner(owner);
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _propertyService.GetAllOwners();
        }

        public void AddInsurer(Insurer insurer)
        {
            _propertyService.AddInsurer(insurer);
        }

        public void AddBroker(Broker broker)
        {
            _propertyService.AddBroker(broker);
        }

        public void AddInsurance(NewInsuranceViewModel insuranceViewModel)
        {
            _propertyService.AddInsurance(new Insurance
            {
                PropertyId = insuranceViewModel.PropertyId,
                StartDate = insuranceViewModel.StartDate,
                EndDate = insuranceViewModel.EndDate,
                BrokerId = insuranceViewModel.Broker.BrokerId,
                InsurerId = insuranceViewModel.Insurer.InsurerId
            });
        }

        public InsuranceViewModel GetLatestInsurance(int propertyId)
        {
            return CreateInsuranceViewModel(_propertyService.GetLatestInsurance(propertyId));
        }

        public void AddElectricalInspectionCertificate(ElectricalInspectionCertificate newCertificate)
        {
            _propertyService.AddElectricalInspectionCertificate(newCertificate);
        }

        public ElectricalInspectionCertificate GetLatestElectricalInspectionCertificate(int propertyId)
        {
            return _propertyService.GetLatestElectricalInspectionCertificate(propertyId);
        }

        public void AddGasSafetyCertificate(GasSafetyCertificate newCertificate)
        {
            _propertyService.AddGasSafetyCertificate(newCertificate);
        }

        public GasSafetyCertificate GetLatestGasSafetyCertificate(int propertyId)
        {
            return _propertyService.GetLatestGasSafetyCertificate(propertyId);
        }

        public void AddEnergyPerformanceCertificate(EnergyPerformanceCertificate newCertificate)
        {
            _propertyService.AddEnergyPerformanceCertificate(newCertificate);
        }

        public EnergyPerformanceCertificate GetLatestEnergyPerformanceCertificate(int propertyId)
        {
            return _propertyService.GetLatestEnergyPerformanceCertificate(propertyId);
        }

        public void AddExpense(Expense expense)
        {
            _propertyService.AddExpense(expense);
        }

        public void AddImprovement(Improvement improvement)
        {
            _propertyService.AddImprovement(improvement);
        }

        public void EditImprovements(IEnumerable<Improvement> improvements)
        {
            _propertyService.EditImprovements(improvements);
        }

        public void DeleteImprovements(IEnumerable<Improvement> improvements)
        {
            _propertyService.DeleteImprovements(improvements);
        }

        public void EditExpenses(IEnumerable<Expense> expenses)
        {
            _propertyService.EditExpenses(expenses);
        }

        public void DeleteExpenses(IEnumerable<Expense> expenses)
        {
            _propertyService.DeleteExpenses(expenses);
        }

        private PropertyDetailsViewModel CreatePropertyDetailsViewModel(int propertyId)
        {
            var property = _propertyService.GetProperty(propertyId);

            var tenancy = _propertyService.GetCurrentTenancy(propertyId);

            var insurance = _propertyService.GetLatestInsurance(propertyId);

            return new PropertyDetailsViewModel
            {
                PropertyId = property.PropertyId,
                PropertyType = property.PropertyType,
                Owner = _propertyService.GetOwner(property.OwnerId)?.Name,
                PurchaseDate = property.PurchaseDate,
                PurchasePrice = property.PurchasePrice,
                StreetAddress1 = property.StreetAddress1,
                StreetAddress2 = property.StreetAddress2,
                Locality = property.Locality,
                City = property.City,
                County = property.County,
                Country = property.Country,
                Postcode = property.Postcode,
                CurrentTenancy = tenancy == null ? null : CreateTenancyViewModel(tenancy),
                ElectricalInspectionCertificate = _propertyService.GetLatestElectricalInspectionCertificate(propertyId),
                GasSafetyCertificate = _propertyService.GetLatestGasSafetyCertificate(propertyId),
                EnergyPerformanceCertificate = _propertyService.GetLatestEnergyPerformanceCertificate(propertyId),
                Insurance = insurance == null ? null : CreateInsuranceViewModel(insurance),
                Expenses = new ObservableCollection<Expense>(_propertyService.GetExpenses(propertyId)),
                Improvements = new ObservableCollection<Improvement>(_propertyService.GetImprovements(propertyId)),
                PurchaseCosts = _propertyService.GetPurchaseCosts(propertyId).ToList()
            };
        }

        private InsuranceViewModel CreateInsuranceViewModel(Insurance insurance) => new InsuranceViewModel
        {
            StartDate = insurance.StartDate,
            EndDate = insurance.EndDate,
            Insurer = _propertyService.GetInsurer(insurance.InsurerId),
            Broker = _propertyService.GetBroker(insurance.BrokerId)
        };

        private TenancyViewModel CreateTenancyViewModel(Tenancy tenancy)
        {
            double? accountBalance = tenancy == null ? (double?)null : _rentAssistant.GetAccountBalance(tenancy.TenancyId);
            double? outstandingBalance = accountBalance == null ? null : (accountBalance >= 0 ? 0 : -1 * accountBalance);

            return new TenancyViewModel
            {
                TenancyId = tenancy.TenancyId,
                StartDate = tenancy.StartDate,
                EndDate = tenancy.EndDate,
                Tenants = new List<Tenant>(_propertyService.GetTenants(tenancy.TenancyId)),
                NextPaymentDue = _rentAssistant.GetNextPaymentDate(tenancy.TenancyId),
                OutstandingBalance = outstandingBalance
            };
        }

        private TenancyDetailsViewModel CreateTenancyDetailsViewModel(int tenancyId)
        {
            var tenancy = _propertyService.GetTenancy(tenancyId);

            var property = _propertyService.GetProperty(tenancy.PropertyId);

            double accountBalance = _rentAssistant.GetAccountBalance(tenancy.TenancyId);
            double outstandingBalance = accountBalance >= 0 ? 0 : -1 * accountBalance;

            return new TenancyDetailsViewModel
            {
                TenancyId = tenancy.TenancyId,
                PropertyId = tenancy.PropertyId,
                StreetAddress1 = property.StreetAddress1,
                StreetAddress2 = property.StreetAddress2,
                Locality = property.Locality,
                City = property.City,
                County = property.County,
                Country = property.Country,
                Postcode = property.Postcode,
                CurrentTenants = new List<Tenant>(_propertyService.GetTenants(tenancy.TenancyId)),
                FirstPayment = tenancy.FirstPayment,
                NextPaymentDue = _rentAssistant.GetNextPaymentDate(tenancy.TenancyId),
                OutstandingBalance = outstandingBalance,
                StartDate = tenancy.StartDate,
                EndDate = tenancy.EndDate,
                PaymentFrequency = tenancy.PaymentFrequency,
                Rates = new List<Rate>(_propertyService.GetRates(tenancyId)),
                AccountEntries = _rentAssistant.GetAllAccountEntries(tenancyId)
            };
        }

        private PropertySummaryViewModel CreatePropertySummaryViewModel(Property property)
        {
            int propertyId = property.PropertyId;

            var electricalCertificate = _propertyService.GetLatestElectricalInspectionCertificate(propertyId);
            var gasCertificate = _propertyService.GetLatestGasSafetyCertificate(propertyId);
            var energyCertificate = _propertyService.GetLatestEnergyPerformanceCertificate(propertyId);
            var insurance = _propertyService.GetLatestInsurance(propertyId);

            var tenancy = _propertyService.GetCurrentTenancy(propertyId);
            var tenants = tenancy == null ? null : _propertyService.GetTenants(tenancy.TenancyId)
                .Select(t => t.Name)
                .ToList();

            double? accountBalance = tenancy == null ? (double?)null : _rentAssistant.GetAccountBalance(tenancy.TenancyId);
            double? outstandingBalance = accountBalance == null ? null : (accountBalance >= 0 ? 0 : -1 * accountBalance);

            DateTime? nextPaymentDate = tenancy == null ? null : _rentAssistant.GetNextPaymentDate(tenancy.TenancyId);

            return new PropertySummaryViewModel
            {
                PropertyId = property.PropertyId,
                StreetAddress1 = property.StreetAddress1,
                StreetAddress2 = property.StreetAddress2,
                Locality = property.Locality,
                City = property.City,
                County = property.County,
                Country = property.Country,
                Postcode = property.Postcode,
                ElectricalInspectionExpiry = electricalCertificate?.ExpiryDate,
                GasSafetyExpiry = gasCertificate?.ExpiryDate,
                EpcExpiry = energyCertificate?.ExpiryDate,
                InsuranceEndDate = insurance?.EndDate,
                NextPaymentDue = nextPaymentDate,
                OutstandingBalance = outstandingBalance,
                Tenants = tenants
            };
        }

        private AddPropertyViewModel CreateAddPropertyViewModel() => new AddPropertyViewModel
        {
            Owners = new ObservableCollection<Owner>(_propertyService.GetAllOwners())
        };

        private AddTenancyViewModel CreateAddTenancyViewModel(int propertyId) => new AddTenancyViewModel
        {
            PropertyId = propertyId,
            Agents = new ObservableCollection<Agent>(_propertyService.GetAllAgents())
        };

        private EditTenancyViewModel CreateEditTenancyViewModel(int tenancyId)
        {
            var tenancy = _propertyService.GetTenancy(tenancyId);

            var agents = _propertyService.GetAllAgents();

            return new EditTenancyViewModel
            {
                TenancyId = tenancyId,
                Tenants = new ObservableCollection<Tenant>(_propertyService.GetTenants(tenancyId)),
                //Agent = agents.Where(a => a.AgentId == tenancy.AgentId).First(),
                AgentId = tenancy.AgentId,
                FirstPayment = tenancy.FirstPayment,
                PaymentFrequency = tenancy.PaymentFrequency,
                DepositAmount = tenancy.Deposit,
                StartDate = tenancy.StartDate,
                EndDate = tenancy.EndDate,
                Rates = new ObservableCollection<Rate>(_propertyService.GetRates(tenancyId)),
                Agents = new ObservableCollection<Agent>(agents)
            };
        }
    }
}
