using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using PropertyManagementData;
using PropertyManagementService.Model;
using Microsoft.EntityFrameworkCore;
using AgentEntity = PropertyManagementData.Model.Agent;
using BrokerEntity = PropertyManagementData.Model.Broker;
using InsurerEntity = PropertyManagementData.Model.Insurer;
using OwnerEntity = PropertyManagementData.Model.Owner;
using PropertyEntity = PropertyManagementData.Model.Property;
using TenancyEntity = PropertyManagementData.Model.Tenancy;
using ScheduledPaymentEntity = PropertyManagementData.Model.ScheduledPayment;
using InsurancePolicyEntity = PropertyManagementData.Model.InsurancePolicy;
using ElectricalInspectionCertificateEntity = PropertyManagementData.Model.ElectricalInspectionCertificate;
using GasSafetyCertificateEntity = PropertyManagementData.Model.GasSafetyCertificate;
using EnergyPerformanceCertificateEntity = PropertyManagementData.Model.EnergyPerformanceCertificate;
using ExpenseEntity = PropertyManagementData.Model.Expense;
using ImprovementEntity = PropertyManagementData.Model.Improvement;
using AccountEntryEntity = PropertyManagementData.Model.AccountEntry;

namespace PropertyManagementService
{
    public class PropertyService : IDisposable
    {
        private PropertyManagementContext _db;

        private IMapper _mapper;

        public PropertyService(PropertyManagementContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<Agent> GetAllAgents()
        {
            return _db.Agent.Select(a => _mapper.Map<Agent>(a))
                .ToList();
        }

        public Agent GetAgent(int agentId)
        {
            return _mapper.Map<Agent>(_db.Agent.Find(agentId));
        }

        public void AddAgent(Agent agent)
        {
            _db.Agent.Add(_mapper.Map<AgentEntity>(agent));
            _db.SaveChanges();
        }

        public List<Broker> GetAllBrokers()
        {
            return _db.Broker.Select(b => _mapper.Map<Broker>(b))
                .ToList();
        }

        public Broker GetBroker(int brokerId)
        {
            return _mapper.Map<Broker>(_db.Broker.Find(brokerId));
        }

        public void AddBroker(Broker broker)
        {
            _db.Broker.Add(_mapper.Map<BrokerEntity>(broker));
            _db.SaveChanges();
        }

        public List<Owner> GetAllOwners()
        {
            return _db.Owner.Select(i => _mapper.Map<Owner>(i))
                .ToList();
        }

        public Owner GetOwner(int ownerId)
        {
            return _mapper.Map<Owner>(_db.Broker.Find(ownerId));
        }

        public void AddOwner(Owner owner)
        {
            _db.Owner.Add(_mapper.Map<OwnerEntity>(owner));
            _db.SaveChanges();
        }

        public List<Insurer> GetAllInsurers()
        {
            return _db.Insurer.Select(i => _mapper.Map<Insurer>(i))
                .ToList();
        }

        public Insurer GetInsurer(int insurerId)
        {
            return _mapper.Map<Insurer>(_db.Insurer.Find(insurerId));
        }

        public void AddInsurer(Insurer insurer)
        {
            _db.Insurer.Add(_mapper.Map<InsurerEntity>(insurer));
            _db.SaveChanges();
        }

        public IEnumerable<Property> GetAllProperties()
        {
            return _mapper.Map<IEnumerable<Property>>(_db.Property);
        }

        public Property GetProperty(int propertyId)
        {
            return _mapper.Map<Property>(_db.Property.Find(propertyId));
        }

        public void AddProperty(Property property)
        {
            var propertyEntity = _mapper.Map<PropertyEntity>(property);
            propertyEntity.Owner = _mapper.Map<OwnerEntity>(property.Owner);

            _db.Property.Update(propertyEntity);

            if (propertyEntity.Image != null)
                _db.Entry(propertyEntity.Image).Property(i => i.FileName).IsModified = false;

            _db.SaveChanges();
        }

        public void UpdateProperty(Property property)
        {
            var propertyEntity = _db.Property.Find(property.PropertyId);
            _mapper.Map(property, propertyEntity);

            if (property.Owner.OwnerId == propertyEntity.Owner.OwnerId)
                _mapper.Map(property.Owner, propertyEntity.Owner);
            else
                propertyEntity.Owner = _mapper.Map<OwnerEntity>(property.Owner);

            if (propertyEntity.Image != null)
                _db.Entry(propertyEntity.Image).Property(i => i.FileName).IsModified = false;

            _db.SaveChanges();
        }

        public Tenancy GetTenancy(int tenancyId)
        {
            return _mapper.Map<Tenancy>(_db.Tenancy.Find(tenancyId));
        }

        public void AddTenancy(Tenancy tenancy)
        {
            var tenancyEntity = _mapper.Map<TenancyEntity>(tenancy);
            tenancyEntity.Agent = _mapper.Map<AgentEntity>(tenancy.Agent);

            _db.Property.Find(tenancy.PropertyId)
                .Tenancies.Add(tenancyEntity);
            _db.SaveChanges();
        }

        public void UpdateTenancy(Tenancy tenancy)
        {
            var tenancyEntity = _db.Tenancy.Find(tenancy.TenancyId);
            _mapper.Map(tenancy, tenancyEntity);

            if (tenancy.Agent.AgentId == tenancyEntity.AgentId)
                _mapper.Map(tenancy.Agent, tenancyEntity.Agent);
            else
                tenancyEntity.Agent = _mapper.Map<AgentEntity>(tenancy.Agent);
            
            _db.SaveChanges();
        }

        public void AddAccountEntry(int tenancyId, AccountEntry accountEntry)
        {
            _db.Tenancy.Find(tenancyId)
                .AccountEntries.Add(_mapper.Map<AccountEntryEntity>(accountEntry));
            _db.SaveChanges();
        }

        public IEnumerable<ScheduledPayment> GetActionableScheduledPayments()
        {
            return _db.ScheduledPayment
                .Where(p => !p.Processed && p.Date <= DateTime.Today)
                .OrderBy(p => p.Date)
                .Select(p => _mapper.Map<ScheduledPayment>(p));
        }

        public void AddScheduledPayments(IEnumerable<ScheduledPayment> scheduledPayments)
        {
            _db.ScheduledPayment.AddRange(scheduledPayments.Select(p => _mapper.Map<ScheduledPaymentEntity>(p)));
        }

        public void UpdateScheduledPayment(ScheduledPayment scheduledPayment)
        {
            var scheduledPaymentEntity = _db.ScheduledPayment.Find(scheduledPayment.ScheduledPaymentId);
            _mapper.Map(scheduledPayment, scheduledPaymentEntity);
            _db.ScheduledPayment.Update(scheduledPaymentEntity);
            _db.SaveChanges();
        }

        public InsurancePolicy GetLatestInsurancePolicy(int propertyId)
        {
            return _mapper.Map<InsurancePolicy>(_db.Insurance
                .Where(i => i.PropertyId == propertyId && i.StartDate <= DateTime.Today)
                .OrderByDescending(i => i.EndDate)
                .FirstOrDefault());
        }

        public void AddInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            _db.Property.Find(insurancePolicy.PropertyId)
                .InsurancePolicies.Add(_mapper.Map<InsurancePolicyEntity>(insurancePolicy));
            _db.SaveChanges();
        }

        public ElectricalInspectionCertificate GetLatestElectricalInspectionCertificate(int propertyId)
        {
            return _mapper.Map<ElectricalInspectionCertificate>(_db.ElectricalInspectionCertificate
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault());
        }

        public void AddElectricalInspectionCertificate(int propertyId, ElectricalInspectionCertificate certificate)
        {
            _db.Property.Find(propertyId)
                .ElectricalInspectionCertificates.Add(_mapper.Map<ElectricalInspectionCertificateEntity>(certificate));
            _db.SaveChanges();
        }

        public GasSafetyCertificate GetLatestGasSafetyCertificate(int propertyId)
        {
            return _mapper.Map<GasSafetyCertificate>(_db.GasSafetyCertificate
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault());
        }

        public void AddGasSafetyCertificate(int propertyId, GasSafetyCertificate certificate)
        {
            _db.Property.Find(propertyId)
                .GasSafetyCertificates.Add(_mapper.Map<GasSafetyCertificateEntity>(certificate));
            _db.SaveChanges();
        }

        public void AddImprovement(int propertyId, Improvement improvement)
        {
            _db.Property.Find(propertyId)
                .Improvements.Add(_mapper.Map<ImprovementEntity>(improvement));
            _db.SaveChanges();
        }

        public void UpdateImprovements(int propertyId, IEnumerable<Improvement> improvements)
        {
            var propertyEntity = _db.Property.Find(propertyId);
            _mapper.Map(improvements, propertyEntity.Improvements);
            //_db.Property.Update(propertyEntity);
            _db.SaveChanges();
        }

        public void AddExpense(int propertyId, Expense expense)
        {
            _db.Property.Find(propertyId)
                .Expenses.Add(_mapper.Map<ExpenseEntity>(expense));
            _db.SaveChanges();
        }

        public void UpdateExpenses(int propertyId, IEnumerable<Expense> expenses)
        {
            var propertyEntity = _db.Property.Find(propertyId);
            _mapper.Map(expenses, propertyEntity.Expenses);
            //_db.Property.Update(propertyEntity);
            _db.SaveChanges();
        }

        public EnergyPerformanceCertificate GetLatestEnergyPerformanceCertificate(int propertyId)
        {
            return _mapper.Map<EnergyPerformanceCertificate>(_db.EnergyPerformanceCertificate
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault());
        }

        public void AddEnergyPerformanceCertificate(int propertyId, EnergyPerformanceCertificate certificate)
        {
            _db.Property.Find(propertyId)
                .EnergyPerformanceCertificates.Add(_mapper.Map<EnergyPerformanceCertificateEntity>(certificate));
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
