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
        private IDbContextFactory<PropertyManagementContext> _dbContextFactory;

        private IMapper _mapper;

        public PropertyService(IDbContextFactory<PropertyManagementContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public List<Agent> GetAllAgents()
        {
            using var db = _dbContextFactory.CreateDbContext();

            return db.Agent.Select(a => _mapper.Map<Agent>(a))
                .ToList();
        }

        public Agent GetAgent(int agentId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<Agent>(db.Agent.Find(agentId));
        }

        public void AddAgent(Agent agent)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Agent.Add(_mapper.Map<AgentEntity>(agent));
            db.SaveChanges();
        }

        public List<Broker> GetAllBrokers()
        {
            using var db = _dbContextFactory.CreateDbContext();

            return db.Broker.Select(b => _mapper.Map<Broker>(b))
                .ToList();
        }

        public Broker GetBroker(int brokerId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<Broker>(db.Broker.Find(brokerId));
        }

        public void AddBroker(Broker broker)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Broker.Add(_mapper.Map<BrokerEntity>(broker));
            db.SaveChanges();
        }

        public List<Owner> GetAllOwners()
        {
            using var db = _dbContextFactory.CreateDbContext();

            return db.Owner.Select(i => _mapper.Map<Owner>(i))
                .ToList();
        }

        public Owner GetOwner(int ownerId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<Owner>(db.Broker.Find(ownerId));
        }

        public void AddOwner(Owner owner)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Owner.Add(_mapper.Map<OwnerEntity>(owner));
            db.SaveChanges();
        }

        public List<Insurer> GetAllInsurers()
        {
            using var db = _dbContextFactory.CreateDbContext();

            return db.Insurer.Select(i => _mapper.Map<Insurer>(i))
                .ToList();
        }

        public Insurer GetInsurer(int insurerId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<Insurer>(db.Insurer.Find(insurerId));
        }

        public void AddInsurer(Insurer insurer)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Insurer.Add(_mapper.Map<InsurerEntity>(insurer));
            db.SaveChanges();
        }

        public IEnumerable<Property> GetAllProperties()
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<IEnumerable<Property>>(db.Property);
        }

        public Property GetProperty(int propertyId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<Property>(db.Property.Find(propertyId));
        }

        public void AddProperty(Property property)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var propertyEntity = _mapper.Map<PropertyEntity>(property);
            propertyEntity.Owner = _mapper.Map<OwnerEntity>(property.Owner);

            db.Property.Update(propertyEntity);

            if (propertyEntity.Image != null)
                db.Entry(propertyEntity.Image).Property(i => i.FileName).IsModified = false;

            db.SaveChanges();
        }

        public void UpdateProperty(Property property)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var propertyEntity = db.Property.Find(property.PropertyId);
            _mapper.Map(property, propertyEntity);

            if (property.Owner.OwnerId == propertyEntity.Owner.OwnerId)
                _mapper.Map(property.Owner, propertyEntity.Owner);
            else
                propertyEntity.Owner = _mapper.Map<OwnerEntity>(property.Owner);

            if (propertyEntity.Image != null)
                db.Entry(propertyEntity.Image).Property(i => i.FileName).IsModified = false;

            db.SaveChanges();
        }

        public Tenancy GetTenancy(int tenancyId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<Tenancy>(db.Tenancy.Find(tenancyId));
        }

        public void AddTenancy(Tenancy tenancy)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var tenancyEntity = _mapper.Map<TenancyEntity>(tenancy);
            tenancyEntity.Agent = _mapper.Map<AgentEntity>(tenancy.Agent);

            db.Property.Find(tenancy.PropertyId)
                .Tenancies.Add(tenancyEntity);
            db.SaveChanges();
        }

        public void UpdateTenancy(Tenancy tenancy)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var tenancyEntity = db.Tenancy.Find(tenancy.TenancyId);
            _mapper.Map(tenancy, tenancyEntity);

            if (tenancy.Agent.AgentId == tenancyEntity.AgentId)
                _mapper.Map(tenancy.Agent, tenancyEntity.Agent);
            else
                tenancyEntity.Agent = _mapper.Map<AgentEntity>(tenancy.Agent);
            
            db.SaveChanges();
        }

        public void AddAccountEntry(int tenancyId, AccountEntry accountEntry)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Tenancy.Find(tenancyId)
                .AccountEntries.Add(_mapper.Map<AccountEntryEntity>(accountEntry));
            db.SaveChanges();
        }

        public void AddScheduledPayments(IEnumerable<ScheduledPayment> scheduledPayments)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.ScheduledPayment.AddRange(scheduledPayments.Select(p => _mapper.Map<ScheduledPaymentEntity>(p)));
        }

        public void UpdateScheduledPayment(ScheduledPayment scheduledPayment)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var scheduledPaymentEntity = db.ScheduledPayment.Find(scheduledPayment.ScheduledPaymentId);
            _mapper.Map(scheduledPayment, scheduledPaymentEntity);
            db.ScheduledPayment.Update(scheduledPaymentEntity);
            db.SaveChanges();
        }

        public InsurancePolicy GetLatestInsurancePolicy(int propertyId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<InsurancePolicy>(db.Insurance
                .Where(i => i.PropertyId == propertyId && i.StartDate <= DateTime.Today)
                .OrderByDescending(i => i.EndDate)
                .FirstOrDefault());
        }

        public void AddInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Property.Find(insurancePolicy.PropertyId)
                .InsurancePolicies.Add(_mapper.Map<InsurancePolicyEntity>(insurancePolicy));
            db.SaveChanges();
        }

        public ElectricalInspectionCertificate GetLatestElectricalInspectionCertificate(int propertyId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<ElectricalInspectionCertificate>(db.ElectricalInspectionCertificate
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault());
        }

        public void AddElectricalInspectionCertificate(int propertyId, ElectricalInspectionCertificate certificate)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Property.Find(propertyId)
                .ElectricalInspectionCertificates.Add(_mapper.Map<ElectricalInspectionCertificateEntity>(certificate));
            db.SaveChanges();
        }

        public GasSafetyCertificate GetLatestGasSafetyCertificate(int propertyId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<GasSafetyCertificate>(db.GasSafetyCertificate
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault());
        }

        public void AddGasSafetyCertificate(int propertyId, GasSafetyCertificate certificate)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Property.Find(propertyId)
                .GasSafetyCertificates.Add(_mapper.Map<GasSafetyCertificateEntity>(certificate));
            db.SaveChanges();
        }

        public void AddImprovement(int propertyId, Improvement improvement)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Property.Find(propertyId)
                .Improvements.Add(_mapper.Map<ImprovementEntity>(improvement));
            db.SaveChanges();
        }

        public void UpdateImprovements(int propertyId, IEnumerable<Improvement> improvements)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var propertyEntity = db.Property.Find(propertyId);
            _mapper.Map(improvements, propertyEntity.Improvements);
            db.SaveChanges();
        }

        public void AddExpense(int propertyId, Expense expense)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Property.Find(propertyId)
                .Expenses.Add(_mapper.Map<ExpenseEntity>(expense));
            db.SaveChanges();
        }

        public void UpdateExpenses(int propertyId, IEnumerable<Expense> expenses)
        {
            using var db = _dbContextFactory.CreateDbContext();

            var propertyEntity = db.Property.Find(propertyId);
            _mapper.Map(expenses, propertyEntity.Expenses);
            db.SaveChanges();
        }

        public EnergyPerformanceCertificate GetLatestEnergyPerformanceCertificate(int propertyId)
        {
            using var db = _dbContextFactory.CreateDbContext();

            return _mapper.Map<EnergyPerformanceCertificate>(db.EnergyPerformanceCertificate
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault());
        }

        public void AddEnergyPerformanceCertificate(int propertyId, EnergyPerformanceCertificate certificate)
        {
            using var db = _dbContextFactory.CreateDbContext();

            db.Property.Find(propertyId)
                .EnergyPerformanceCertificates.Add(_mapper.Map<EnergyPerformanceCertificateEntity>(certificate));
            db.SaveChanges();
        }

        public void Dispose()
        {
            //db.Dispose();
        }
    }
}
