using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Navigation;
using AutoMapper;
using PropertyManagementCommon;
using PropertyManagementService;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi
{
    public class AppController
    {
        private NavigationWindow _mainWindow;

        private ServiceProvider _serviceProvider;

        private RateAssistant _rateAssistant;

        private IMapper _mapper;

        public AppController(NavigationWindow mainWindow, ServiceProvider serviceProvider, RateAssistant rateAssistant, IMapper mapper)
        {
            _mainWindow = mainWindow;
            _serviceProvider = serviceProvider;

            _rateAssistant = rateAssistant;
            _mapper = mapper;
        }

        public void IndexPage()
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            var viewModel = new IndexViewModel
            {
                Properties = _mapper.Map<ObservableCollection<PropertySummaryViewModel>>(propertyService.GetAllProperties())
            };

            _mainWindow.Navigate(new IndexPage(this, viewModel));
        }

        public void AddPropertyPage()
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            var viewModel = new AddPropertyViewModel
            {
                Owners = new ObservableCollection<Owner>(propertyService.GetAllOwners())
            };

            _mainWindow.Navigate(new AddPropertyPage(this, viewModel));
        }

        public void PropertyDetailsPage(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            using var imageService = _serviceProvider.GetImageService();

            var viewModel = _mapper.Map<PropertyDetailsViewModel>(propertyService.GetProperty(propertyId));

            if (viewModel.ImageId is int imageId)
                viewModel.ImagePath = imageService.GetImagePath(imageId);

            _mainWindow.Navigate(new PropertyDetailsPage(this, viewModel));
        }

        public void AddTenancyPage(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            var viewModel = new AddTenancyViewModel
            {
                PropertyId = propertyId,
                Agents = new ObservableCollection<Agent>(propertyService.GetAllAgents())
            };

            _mainWindow.Navigate(new AddTenancyPage(this, viewModel));
        }

        public void EditProperty(EditPropertyViewModel viewModel)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.UpdateProperty(_mapper.Map<Property>(viewModel));

            PropertyDetailsPage(viewModel.PropertyId);
        }

        public void AddPayment(int tenancyId, AccountEntry newPayment)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddAccountEntry(tenancyId, newPayment);
        }

        public void EditTenancyPage(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            var viewModel = _mapper.Map<EditTenancyViewModel>(propertyService.GetTenancy(propertyId));
            viewModel.Agents = new ObservableCollection<Agent>(propertyService.GetAllAgents());

            _mainWindow.Navigate(new EditTenancyPage(this, viewModel));
        }

        public void TenancyDetailsPage(int tenancyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            var viewModel = _mapper.Map<TenancyDetailsViewModel>(propertyService.GetTenancy(tenancyId));

            _mainWindow.Navigate(new TenancyDetailsPage(this, viewModel));
        }

        public void AddProperty(AddPropertyViewModel viewModel)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddProperty(_mapper.Map<Property>(viewModel));

            IndexPage();
        }

        public void AddImage(string sourcePath, AddPropertyViewModel viewModel)
        {
            using var imageService = _serviceProvider.GetImageService();

            if (viewModel.ImageId is int oldImageId)
            {
                viewModel.ImagePath = null;
                imageService.DeleteImage(oldImageId);
            }

            int imageId = imageService.SaveImage(sourcePath);

            viewModel.ImageId = imageId;
            viewModel.ImagePath = imageService.GetImagePath(imageId);
        }

        public void ClearImage(AddPropertyViewModel viewModel)
        {
            if (viewModel.ImageId is not int imageId) return;

            using var imageService = _serviceProvider.GetImageService();
            
            viewModel.ImagePath = null;
            viewModel.ImageId = null;
            imageService.DeleteImage(imageId);
        }

        public void AddImage(string sourcePath, EditPropertyViewModel viewModel)
        {
            using var imageService = _serviceProvider.GetImageService();

            if (viewModel.ImageId is int oldImageId)
            {
                viewModel.ImagePath = null;
                imageService.DeleteImage(oldImageId);
            }

            int imageId = imageService.SaveImage(sourcePath);

            viewModel.ImageId = imageId;
            viewModel.ImagePath = imageService.GetImagePath(imageId);
        }

        public void ClearImage(EditPropertyViewModel viewModel)
        {
            if (viewModel.ImageId is not int imageId) return;

            using var imageService = _serviceProvider.GetImageService();

            viewModel.ImagePath = null;
            viewModel.ImageId = null;
            imageService.DeleteImage(imageId);
        }

        public void UpdateTenancy(EditTenancyViewModel editTenancyViewModel)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            var tenancy = propertyService.GetTenancy(editTenancyViewModel.TenancyId);

            var oldRates = tenancy.Rates;
            var newRates = editTenancyViewModel.Rates;

            bool haveRatesChanged = _rateAssistant.HaveRatesChanged(oldRates, newRates);

            _mapper.Map(editTenancyViewModel, tenancy);

            if (haveRatesChanged)
            {
                tenancy.ScheduledPayments.RemoveAll(p => p.Date >= DateTime.Today);
                tenancy.ScheduledPayments.AddRange(
                    _rateAssistant.CreateScheduledPaymentsAfterDate(tenancy, DateTime.Today));
            }

            propertyService.UpdateTenancy(tenancy);

            TenancyDetailsPage(editTenancyViewModel.TenancyId);
        }

        public void AddTenancy(AddTenancyViewModel viewModel)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            var tenancy = _mapper.Map<Tenancy>(viewModel);

            tenancy.AccountEntries.Add(new AccountEntry
            {
                Amount = tenancy.Deposit * -1,
                Date = tenancy.StartDate,
                Description = "Deposit due",
                Kind = AccountEntryKind.AmountOwed
            });

            tenancy.ScheduledPayments.AddRange(
                _rateAssistant.CreateScheduledPayments(tenancy));

            propertyService.AddTenancy(tenancy);

            PropertyDetailsPage(viewModel.PropertyId);
        }

        public IEnumerable<Insurer> GetAllInsurers()
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            return propertyService.GetAllInsurers();
        }

        public void AddAgent(Agent agent)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddAgent(agent);
        }

        public IEnumerable<Broker> GetAllBrokers()
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            return propertyService.GetAllBrokers();
        }

        public void AddOwner(Owner owner)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddOwner(_mapper.Map<Owner>(owner));
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            return propertyService.GetAllOwners();
        }

        public void AddInsurer(Insurer insurer)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddInsurer(insurer);
        }

        public void AddBroker(Broker broker)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddBroker(broker);
        }

        public NewInsurancePolicyViewModel GetNewInsurancePolicyViewModel(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            return new NewInsurancePolicyViewModel
            {
                PropertyId = propertyId,
                Brokers = propertyService.GetAllBrokers(),
                Insurers = propertyService.GetAllInsurers()
            };
        }

        public void AddInsurancePolicy(NewInsurancePolicyViewModel viewModel)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddInsurancePolicy(_mapper.Map<InsurancePolicy>(viewModel));
        }

        public InsurancePolicy GetLatestInsurancePolicy(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            return propertyService.GetLatestInsurancePolicy(propertyId);
        }

        public void AddElectricalInspectionCertificate(int propertyId, ElectricalInspectionCertificate certificate)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddElectricalInspectionCertificate(propertyId, certificate);
        }

        public void EditPropertyPage(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();
            using var imageService = _serviceProvider.GetImageService();

            var property = propertyService.GetProperty(propertyId);

            var viewModel = _mapper.Map<EditPropertyViewModel>(property);
            viewModel.Owners = new ObservableCollection<Owner>(propertyService.GetAllOwners());

            if (viewModel.ImageId is int imageId)
                viewModel.ImagePath = imageService.GetImagePath(imageId);

            _mainWindow.Navigate(new EditPropertyPage(this, viewModel));
        }

        public ElectricalInspectionCertificate GetLatestElectricalInspectionCertificate(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();
            
            return propertyService.GetLatestElectricalInspectionCertificate(propertyId);
        }

        public void AddGasSafetyCertificate(int propertyId, GasSafetyCertificate certificate)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddGasSafetyCertificate(propertyId, certificate);
        }

        public GasSafetyCertificate GetLatestGasSafetyCertificate(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();
            
            return propertyService.GetLatestGasSafetyCertificate(propertyId);
        }

        public void AddEnergyPerformanceCertificate(int propertyId, EnergyPerformanceCertificate certificate)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddEnergyPerformanceCertificate(propertyId, certificate);
        }

        public EnergyPerformanceCertificate GetLatestEnergyPerformanceCertificate(int propertyId)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            return propertyService.GetLatestEnergyPerformanceCertificate(propertyId);
        }

        public void AddExpense(int propertyId, Expense expense)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddExpense(propertyId, expense);
        }

        public void AddImprovement(int propertyId, Improvement improvement)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.AddImprovement(propertyId, improvement);
        }

        public void UpdateImprovements(int propertyId, IEnumerable<Improvement> improvements)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.UpdateImprovements(propertyId, improvements);
        }

        public void UpdateExpenses(int propertyId, IEnumerable<Expense> expenses)
        {
            using var propertyService = _serviceProvider.GetPropertyService();

            propertyService.UpdateExpenses(propertyId, expenses);
        }
    }
}
