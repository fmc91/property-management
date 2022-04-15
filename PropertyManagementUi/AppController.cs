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
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace PropertyManagementUi
{
    public class AppController
    {
        public delegate void NavigationDelegate(Page page);

        //private MainWindow _mainWindow;

        //private IServiceProvider _serviceProvider;

        private PropertyService _propertyService;

        private ImageService _imageService;

        private RateAssistant _rateAssistant;

        private IMapper _mapper;

        public AppController(PropertyService propertyService, ImageService imageService, RateAssistant rateAssistant, IMapper mapper)
        {
            //_mainWindow = mainWindow;
            //_serviceProvider = serviceProvider;
            _propertyService = propertyService;
            _imageService = imageService;
            _rateAssistant = rateAssistant;
            _mapper = mapper;
        }

        public NavigationDelegate Navigate { get; set; }

        public void IndexPage()
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            var viewModel = new IndexViewModel
            {
                Properties = _mapper.Map<ObservableCollection<PropertySummaryViewModel>>(_propertyService.GetAllProperties())
            };

            Navigate(new IndexPage(this, viewModel));
        }

        public void AddPropertyPage()
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            var viewModel = new AddPropertyViewModel
            {
                Owners = new ObservableCollection<Owner>(_propertyService.GetAllOwners())
            };

            Navigate(new AddPropertyPage(this, viewModel));
        }

        public void PropertyDetailsPage(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            //using var _imageService = _serviceProvider.GetRequiredService<ImageService>();

            var viewModel = _mapper.Map<PropertyDetailsViewModel>(_propertyService.GetProperty(propertyId));

            if (viewModel.ImageId is int imageId)
                viewModel.ImagePath = _imageService.GetImagePath(imageId);

            Navigate(new PropertyDetailsPage(this, viewModel));
        }

        public void AddTenancyPage(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            var viewModel = new AddTenancyViewModel
            {
                PropertyId = propertyId,
                Agents = new ObservableCollection<Agent>(_propertyService.GetAllAgents())
            };

            Navigate(new AddTenancyPage(this, viewModel));
        }

        public void EditProperty(EditPropertyViewModel viewModel)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.UpdateProperty(_mapper.Map<Property>(viewModel));

            PropertyDetailsPage(viewModel.PropertyId);
        }

        public void AddPayment(int tenancyId, AccountEntry newPayment)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddAccountEntry(tenancyId, newPayment);
        }

        public void EditTenancyPage(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            var viewModel = _mapper.Map<EditTenancyViewModel>(_propertyService.GetTenancy(propertyId));
            viewModel.Agents = new ObservableCollection<Agent>(_propertyService.GetAllAgents());

            Navigate(new EditTenancyPage(this, viewModel));
        }

        public void TenancyDetailsPage(int tenancyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            var viewModel = _mapper.Map<TenancyDetailsViewModel>(_propertyService.GetTenancy(tenancyId));

            Navigate(new TenancyDetailsPage(this, viewModel));
        }

        public void AddProperty(AddPropertyViewModel viewModel)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddProperty(_mapper.Map<Property>(viewModel));

            IndexPage();
        }

        public void AddImage(string sourcePath, AddPropertyViewModel viewModel)
        {
            //using var _imageService = _serviceProvider.GetRequiredService<ImageService>();

            if (viewModel.ImageId is int oldImageId)
            {
                viewModel.ImagePath = null;
                _imageService.DeleteImage(oldImageId);
            }

            int imageId = _imageService.SaveImage(sourcePath);

            viewModel.ImageId = imageId;
            viewModel.ImagePath = _imageService.GetImagePath(imageId);
        }

        public void ClearImage(AddPropertyViewModel viewModel)
        {
            if (viewModel.ImageId is not int imageId) return;

            //using var _imageService = _serviceProvider.GetRequiredService<ImageService>();
            
            viewModel.ImagePath = null;
            viewModel.ImageId = null;
            _imageService.DeleteImage(imageId);
        }

        public void AddImage(string sourcePath, EditPropertyViewModel viewModel)
        {
            //using var _imageService = _serviceProvider.GetRequiredService<ImageService>();

            if (viewModel.ImageId is int oldImageId)
            {
                viewModel.ImagePath = null;
                _imageService.DeleteImage(oldImageId);
            }

            int imageId = _imageService.SaveImage(sourcePath);

            viewModel.ImageId = imageId;
            viewModel.ImagePath = _imageService.GetImagePath(imageId);
        }

        public void ClearImage(EditPropertyViewModel viewModel)
        {
            if (viewModel.ImageId is not int imageId) return;

            //using var _imageService = _serviceProvider.GetRequiredService<ImageService>();

            viewModel.ImagePath = null;
            viewModel.ImageId = null;
            _imageService.DeleteImage(imageId);
        }

        public void UpdateTenancy(EditTenancyViewModel editTenancyViewModel)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            var tenancy = _propertyService.GetTenancy(editTenancyViewModel.TenancyId);

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

            _propertyService.UpdateTenancy(tenancy);

            TenancyDetailsPage(editTenancyViewModel.TenancyId);
        }

        public void AddTenancy(AddTenancyViewModel viewModel)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

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

            _propertyService.AddTenancy(tenancy);

            PropertyDetailsPage(viewModel.PropertyId);
        }

        public IEnumerable<Insurer> GetAllInsurers()
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            return _propertyService.GetAllInsurers();
        }

        public void AddAgent(Agent agent)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddAgent(agent);
        }

        public IEnumerable<Broker> GetAllBrokers()
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            return _propertyService.GetAllBrokers();
        }

        public void AddOwner(Owner owner)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddOwner(_mapper.Map<Owner>(owner));
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            return _propertyService.GetAllOwners();
        }

        public void AddInsurer(Insurer insurer)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddInsurer(insurer);
        }

        public void AddBroker(Broker broker)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddBroker(broker);
        }

        public NewInsurancePolicyViewModel GetNewInsurancePolicyViewModel(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            return new NewInsurancePolicyViewModel
            {
                PropertyId = propertyId,
                Brokers = _propertyService.GetAllBrokers(),
                Insurers = _propertyService.GetAllInsurers()
            };
        }

        public void AddInsurancePolicy(NewInsurancePolicyViewModel viewModel)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddInsurancePolicy(_mapper.Map<InsurancePolicy>(viewModel));
        }

        public InsurancePolicy GetLatestInsurancePolicy(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            return _propertyService.GetLatestInsurancePolicy(propertyId);
        }

        public void AddElectricalInspectionCertificate(int propertyId, ElectricalInspectionCertificate certificate)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddElectricalInspectionCertificate(propertyId, certificate);
        }

        public void EditPropertyPage(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();
            //using var _imageService = _serviceProvider.GetRequiredService<ImageService>();

            var property = _propertyService.GetProperty(propertyId);

            var viewModel = _mapper.Map<EditPropertyViewModel>(property);
            viewModel.Owners = new ObservableCollection<Owner>(_propertyService.GetAllOwners());

            if (viewModel.ImageId is int imageId)
                viewModel.ImagePath = _imageService.GetImagePath(imageId);

            Navigate(new EditPropertyPage(this, viewModel));
        }

        public ElectricalInspectionCertificate GetLatestElectricalInspectionCertificate(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();
            
            return _propertyService.GetLatestElectricalInspectionCertificate(propertyId);
        }

        public void AddGasSafetyCertificate(int propertyId, GasSafetyCertificate certificate)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddGasSafetyCertificate(propertyId, certificate);
        }

        public GasSafetyCertificate GetLatestGasSafetyCertificate(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();
            
            return _propertyService.GetLatestGasSafetyCertificate(propertyId);
        }

        public void AddEnergyPerformanceCertificate(int propertyId, EnergyPerformanceCertificate certificate)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddEnergyPerformanceCertificate(propertyId, certificate);
        }

        public EnergyPerformanceCertificate GetLatestEnergyPerformanceCertificate(int propertyId)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            return _propertyService.GetLatestEnergyPerformanceCertificate(propertyId);
        }

        public void AddExpense(int propertyId, Expense expense)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddExpense(propertyId, expense);
        }

        public void AddImprovement(int propertyId, Improvement improvement)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.AddImprovement(propertyId, improvement);
        }

        public void UpdateImprovements(int propertyId, IEnumerable<Improvement> improvements)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.UpdateImprovements(propertyId, improvements);
        }

        public void UpdateExpenses(int propertyId, IEnumerable<Expense> expenses)
        {
            //using var _propertyService = _serviceProvider.GetRequiredService<PropertyService>();

            _propertyService.UpdateExpenses(propertyId, expenses);
        }
    }
}
