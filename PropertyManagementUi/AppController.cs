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

        private readonly PropertyService _propertyService;

        private readonly ImageService _imageService;

        private readonly RateAssistant _rateAssistant;

        private readonly IMapper _mapper;

        public AppController(PropertyService propertyService, ImageService imageService, RateAssistant rateAssistant, IMapper mapper)
        {
            _propertyService = propertyService;
            _imageService = imageService;
            _rateAssistant = rateAssistant;
            _mapper = mapper;
        }

        public NavigationDelegate Navigate { get; set; }

        public void IndexPage()
        {
            var viewModel = new IndexViewModel
            {
                Properties = _mapper.Map<ObservableCollection<PropertySummaryViewModel>>(_propertyService.GetAllProperties())
            };

            Navigate(new IndexPage(this, viewModel));
        }

        public void AddPropertyPage()
        {
            var viewModel = new AddPropertyViewModel
            {
                Owners = new ObservableCollection<Owner>(_propertyService.GetAllOwners())
            };

            Navigate(new AddPropertyPage(this, viewModel));
        }

        public void PropertyDetailsPage(int propertyId)
        {
            var viewModel = _mapper.Map<PropertyDetailsViewModel>(_propertyService.GetProperty(propertyId));

            if (viewModel.ImageId is int imageId)
                viewModel.ImagePath = _imageService.GetImagePath(imageId);

            Navigate(new PropertyDetailsPage(this, viewModel));
        }

        public void AddTenancyPage(int propertyId)
        {
            var viewModel = new AddTenancyViewModel
            {
                PropertyId = propertyId,
                Agents = new ObservableCollection<Agent>(_propertyService.GetAllAgents())
            };

            Navigate(new AddTenancyPage(this, viewModel));
        }

        public void EditProperty(EditPropertyViewModel viewModel)
        {
            _propertyService.UpdateProperty(_mapper.Map<Property>(viewModel));

            PropertyDetailsPage(viewModel.PropertyId);
        }

        public void AddPayment(int tenancyId, AccountEntry newPayment)
        {
            _propertyService.AddAccountEntry(tenancyId, newPayment);
        }

        public void EditTenancyPage(int propertyId)
        {
            var viewModel = _mapper.Map<EditTenancyViewModel>(_propertyService.GetTenancy(propertyId));
            viewModel.Agents = new ObservableCollection<Agent>(_propertyService.GetAllAgents());

            Navigate(new EditTenancyPage(this, viewModel));
        }

        public void TenancyDetailsPage(int tenancyId)
        {
            var viewModel = _mapper.Map<TenancyDetailsViewModel>(_propertyService.GetTenancy(tenancyId));

            Navigate(new TenancyDetailsPage(this, viewModel));
        }

        public void AddProperty(AddPropertyViewModel viewModel)
        {
            _propertyService.AddProperty(_mapper.Map<Property>(viewModel));

            IndexPage();
        }

        public void AddImage(string sourcePath, AddPropertyViewModel viewModel)
        {
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

            viewModel.ImagePath = null;
            viewModel.ImageId = null;
            _imageService.DeleteImage(imageId);
        }

        public void AddImage(string sourcePath, EditPropertyViewModel viewModel)
        {
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

            viewModel.ImagePath = null;
            viewModel.ImageId = null;
            _imageService.DeleteImage(imageId);
        }

        public void UpdateTenancy(EditTenancyViewModel editTenancyViewModel)
        {
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
            _propertyService.AddOwner(_mapper.Map<Owner>(owner));
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

        public NewInsurancePolicyViewModel GetNewInsurancePolicyViewModel(int propertyId)
        {
            return new NewInsurancePolicyViewModel
            {
                PropertyId = propertyId,
                Brokers = _propertyService.GetAllBrokers(),
                Insurers = _propertyService.GetAllInsurers()
            };
        }

        public void AddInsurancePolicy(NewInsurancePolicyViewModel viewModel)
        {
            _propertyService.AddInsurancePolicy(_mapper.Map<InsurancePolicy>(viewModel));
        }

        public InsurancePolicy GetLatestInsurancePolicy(int propertyId)
        {
            return _propertyService.GetLatestInsurancePolicy(propertyId);
        }

        public void AddElectricalInspectionCertificate(int propertyId, ElectricalInspectionCertificate certificate)
        {
            _propertyService.AddElectricalInspectionCertificate(propertyId, certificate);
        }

        public void EditPropertyPage(int propertyId)
        {
            var property = _propertyService.GetProperty(propertyId);

            var viewModel = _mapper.Map<EditPropertyViewModel>(property);
            viewModel.Owners = new ObservableCollection<Owner>(_propertyService.GetAllOwners());

            if (viewModel.ImageId is int imageId)
                viewModel.ImagePath = _imageService.GetImagePath(imageId);

            Navigate(new EditPropertyPage(this, viewModel));
        }

        public ElectricalInspectionCertificate GetLatestElectricalInspectionCertificate(int propertyId)
        {
            return _propertyService.GetLatestElectricalInspectionCertificate(propertyId);
        }

        public void AddGasSafetyCertificate(int propertyId, GasSafetyCertificate certificate)
        {
            _propertyService.AddGasSafetyCertificate(propertyId, certificate);
        }

        public GasSafetyCertificate GetLatestGasSafetyCertificate(int propertyId)
        {
            return _propertyService.GetLatestGasSafetyCertificate(propertyId);
        }

        public void AddEnergyPerformanceCertificate(int propertyId, EnergyPerformanceCertificate certificate)
        {
            _propertyService.AddEnergyPerformanceCertificate(propertyId, certificate);
        }

        public EnergyPerformanceCertificate GetLatestEnergyPerformanceCertificate(int propertyId)
        {
            return _propertyService.GetLatestEnergyPerformanceCertificate(propertyId);
        }

        public void AddExpense(int propertyId, Expense expense)
        {
            _propertyService.AddExpense(propertyId, expense);
        }

        public void AddImprovement(int propertyId, Improvement improvement)
        {
            _propertyService.AddImprovement(propertyId, improvement);
        }

        public void UpdateImprovements(int propertyId, IEnumerable<Improvement> improvements)
        {
            _propertyService.UpdateImprovements(propertyId, improvements);
        }

        public void UpdateExpenses(int propertyId, IEnumerable<Expense> expenses)
        {
            _propertyService.UpdateExpenses(propertyId, expenses);
        }
    }
}
