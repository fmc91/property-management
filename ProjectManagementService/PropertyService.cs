using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PropertyManagementData;
using PropertyManagementCommon.Model;

namespace PropertyManagementService
{
    public class PropertyService : IDisposable
    {
        private readonly PropertyManagementContext _db;

        public PropertyService(PropertyManagementContext db)
        {
            _db = db;
        }

        public Property GetProperty(int propertyId)
        {
            return _db.Property
                .AsNoTracking()
                .Where(p => p.PropertyId == propertyId)
                .SingleOrDefault();
        }

        public Owner GetOwner(int ownerId)
        {
            return _db.Owner
                .AsNoTracking()
                .Where(o => o.OwnerId == ownerId)
                .SingleOrDefault();
        }

        public Agent GetAgent(int agentId)
        {
            return _db.Agent
                .AsNoTracking()
                .Where(i => i.AgentId == agentId)
                .SingleOrDefault();
        }

        public Insurer GetInsurer(int insurerId)
        {
            return _db.Insurer
                .AsNoTracking()
                .Where(i => i.InsurerId == insurerId)
                .SingleOrDefault();
        }

        public Broker GetBroker(int brokerId)
        {
            return _db.Broker
                .AsNoTracking()
                .Where(b => b.BrokerId == brokerId)
                .SingleOrDefault();
        }

        public Tenancy GetTenancy(int tenancyId)
        {
            return _db.Tenancy
                .AsNoTracking()
                .Where(t => t.TenancyId == tenancyId)
                .SingleOrDefault();
        }

        public IEnumerable<Property> GetAllProperties()
        {
            return _db.Property.AsNoTracking();
        }

        public IEnumerable<Agent> GetAllAgents()
        {
            return _db.Agent.AsNoTracking();
        }

        public IEnumerable<Broker> GetAllBrokers()
        {
            return _db.Broker.AsNoTracking();
        }

        public IEnumerable<Insurer> GetAllInsurers()
        {
            return _db.Insurer.AsNoTracking();
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _db.Owner.AsNoTracking();
        }

        public IEnumerable<Improvement> GetImprovements(int propertyId)
        {
            return _db.Improvement.AsNoTracking()
                .Where(i => i.PropertyId == propertyId);
        }

        public IEnumerable<PurchaseCost> GetPurchaseCosts(int propertyId)
        {
            return _db.PurchaseCost.AsNoTracking()
                .Where(c => c.PropertyId == propertyId);
        }

        public IEnumerable<Expense> GetExpenses(int propertyId)
        {
            return _db.Expense.AsNoTracking()
                .Where(e => e.PropertyId == propertyId);
        }

        public IEnumerable<Tenant> GetTenants(int tenancyId)
        {
            return _db.Tenant.AsNoTracking()
                .Where(t => t.TenancyId == tenancyId);
        }

        public IEnumerable<ActualPayment> GetActualPayments(int tenancyId)
        {
            return _db.ActualPayment.AsNoTracking()
                .Where(p => p.TenancyId == tenancyId);
        }

        public IEnumerable<ScheduledPayment> GetScheduledPayments(int tenancyId)
        {
            return _db.ScheduledPayment.AsNoTracking()
                .Where(p => p.TenancyId == tenancyId);
        }

        public void DeleteScheduledPaymentsAfterDate(int tenancyId, DateTime date)
        {
            var paymentsToDelete = _db.ScheduledPayment
                .Where(p => p.TenancyId == tenancyId && p.Date > date);

            _db.ScheduledPayment.RemoveRange(paymentsToDelete);

            _db.SaveChanges();
        }

        public IEnumerable<Rate> GetRates(int tenancyId)
        {
            return _db.Rate.AsNoTracking()
                .Where(p => p.TenancyId == tenancyId);
        }

        public Rate GetCurrentRate(int tenancyId)
        {
            return _db.Rate.AsNoTracking()
                .Where(rp =>
                    rp.TenancyId == tenancyId &&
                    rp.StartDate <= DateTime.Today &&
                    rp.EndDate >= DateTime.Today)
                .FirstOrDefault();
        }

        public Tenancy GetCurrentTenancy(int propertyId)
        {
            return _db.Tenancy.AsNoTracking()
                .Where(t =>
                    t.PropertyId == propertyId &&
                    t.StartDate <= DateTime.Today &&
                    t.EndDate >= DateTime.Today)
                .FirstOrDefault();
        }

        public void AddImprovement(Improvement improvement)
        {
            _db.Improvement.Add(improvement);
            _db.SaveChanges();

            _db.Entry(improvement).State = EntityState.Detached;
        }

        public void AddPurchaseCost(PurchaseCost purchaseCost)
        {
            _db.PurchaseCost.Add(purchaseCost);
            _db.SaveChanges();

            _db.Entry(purchaseCost).State = EntityState.Detached;
        }

        public void AddPurchaseCosts(IEnumerable<PurchaseCost> purchaseCosts)
        {
            _db.PurchaseCost.AddRange(purchaseCosts);
            _db.SaveChanges();

            foreach (var pc in purchaseCosts)
                _db.Entry(pc).State = EntityState.Detached;
        }

        public void AddExpense(Expense expense)
        {
            _db.Expense.Add(expense);
            _db.SaveChanges();

            _db.Entry(expense).State = EntityState.Detached;
        }

        public void AddOwner(Owner owner)
        {
            _db.Owner.Add(owner);
            _db.SaveChanges();

            _db.Entry(owner).State = EntityState.Detached;
        }

        public void AddTenancy(Tenancy tenancy)
        {
            _db.Tenancy.Add(tenancy);
            _db.SaveChanges();

            _db.Entry(tenancy).State = EntityState.Detached;
        }

        public void AddRates(IEnumerable<Rate> rentPeriods)
        {
            _db.Rate.AddRange(rentPeriods);
            _db.SaveChanges();

            foreach (var rp in rentPeriods)
                _db.Entry(rp).State = EntityState.Detached;
        }

        public void AddScheduledPayment(ScheduledPayment scheduledPayment)
        {
            _db.ScheduledPayment.Add(scheduledPayment);
            _db.SaveChanges();

            _db.Entry(scheduledPayment).State = EntityState.Detached;
        }

        public void AddScheduledPayments(IEnumerable<ScheduledPayment> scheduledPayments)
        {
            _db.ScheduledPayment.AddRange(scheduledPayments);
            _db.SaveChanges();

            foreach (var p in scheduledPayments)
                _db.Entry(p).State = EntityState.Detached;
        }

        public void AddRate(Rate rentPeriod)
        {
            _db.Rate.Add(rentPeriod);
            _db.SaveChanges();

            _db.Entry(rentPeriod).State = EntityState.Detached;
        }

        public void AddTenants(IEnumerable<Tenant> tenants)
        {
            _db.Tenant.AddRange(tenants);
            _db.SaveChanges();

            foreach (var t in tenants)
                _db.Entry(t).State = EntityState.Detached;
        }

        public void AddTenant(Tenant tenant)
        {
            _db.Tenant.Add(tenant);
            _db.SaveChanges();

            _db.Entry(tenant).State = EntityState.Detached;
        }

        public void AddProperty(Property property)
        {
            _db.Property.Add(property);
            _db.SaveChanges();

            _db.Entry(property).State = EntityState.Detached;
        }

        public void AddAgent(Agent agent)
        {
            _db.Agent.Add(agent);
            _db.SaveChanges();

            _db.Entry(agent).State = EntityState.Detached;
        }

        public void AddBroker(Broker broker)
        {
            _db.Broker.Add(broker);
            _db.SaveChanges();

            _db.Entry(broker).State = EntityState.Detached;
        }

        public void AddInsurer(Insurer insurer)
        {
            _db.Insurer.Add(insurer);
            _db.SaveChanges();

            _db.Entry(insurer).State = EntityState.Detached;
        }

        public void AddInsurance(Insurance insurance)
        {
            _db.Insurance.Add(insurance);
            _db.SaveChanges();

            _db.Entry(insurance).State = EntityState.Detached;
        }

        public void AddElectricalInspectionCertificate(ElectricalInspectionCertificate certificate)
        {
            _db.ElectricalInspectionCertificate.Add(certificate);
            _db.SaveChanges();

            _db.Entry(certificate).State = EntityState.Detached;
        }

        public void AddGasSafetyCertificate(GasSafetyCertificate certificate)
        {
            _db.GasSafetyCertificate.Add(certificate);
            _db.SaveChanges();

            _db.Entry(certificate).State = EntityState.Detached;
        }

        public void AddEnergyPerformanceCertificate(EnergyPerformanceCertificate certificate)
        {
            _db.EnergyPerformanceCertificate.Add(certificate);
            _db.SaveChanges();

            _db.Entry(certificate).State = EntityState.Detached;
        }

        public void AddActualPayment(ActualPayment rentPayment)
        {
            _db.ActualPayment.Add(rentPayment);
            _db.SaveChanges();

            _db.Entry(rentPayment).State = EntityState.Detached;
        }

        public Insurance GetLatestInsurance(int propertyId)
        {
            return _db.Insurance.AsNoTracking()
                .Where(c => c.PropertyId == propertyId && c.StartDate <= DateTime.Today)
                .OrderByDescending(c => c.EndDate)
                .FirstOrDefault();
        }

        public ElectricalInspectionCertificate GetLatestElectricalInspectionCertificate(int propertyId)
        {
            return _db.ElectricalInspectionCertificate.AsNoTracking()
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault();
        }

        public GasSafetyCertificate GetLatestGasSafetyCertificate(int propertyId)
        {
            return _db.GasSafetyCertificate.AsNoTracking()
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault();
        }

        public EnergyPerformanceCertificate GetLatestEnergyPerformanceCertificate(int propertyId)
        {
            return _db.EnergyPerformanceCertificate.AsNoTracking()
                .Where(c => c.PropertyId == propertyId && c.IssueDate <= DateTime.Today)
                .OrderByDescending(c => c.ExpiryDate)
                .FirstOrDefault();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void DeleteAllScheduledPayments(int tenancyId)
        {
            var payments = _db.ScheduledPayment.AsNoTracking()
                    .Where(p => p.TenancyId == tenancyId);

            _db.ScheduledPayment.RemoveRange(payments);
        }

        public void EditTenancy(Tenancy tenancy)
        {
            _db.Update(tenancy);
            _db.SaveChanges();

            _db.Entry(tenancy).State = EntityState.Detached;
        }

        public void EditTenants(IEnumerable<Tenant> tenants)
        {
            _db.UpdateRange(tenants);
            _db.SaveChanges();

            foreach (var tenant in tenants) _db.Entry(tenant).State = EntityState.Detached;
        }

        public void DeleteTenants(IEnumerable<Tenant> tenants)
        {
            _db.Tenant.RemoveRange(tenants);
            _db.SaveChanges();
        }

        public void EditRates(IEnumerable<Rate> rates)
        {
            _db.UpdateRange(rates);
            _db.SaveChanges();
            
            foreach (var rate in rates) _db.Entry(rate).State = EntityState.Detached;
        }

        public void DeleteRates(IEnumerable<Rate> rates)
        {
            _db.Rate.RemoveRange(rates);
            _db.SaveChanges();
        }

        public void EditImprovements(IEnumerable<Improvement> improvements)
        {
            _db.UpdateRange(improvements);
            _db.SaveChanges();

            foreach (var improvement in improvements) _db.Entry(improvement).State = EntityState.Detached;
        }

        public void DeleteImprovements(IEnumerable<Improvement> improvements)
        {
            _db.Improvement.RemoveRange(improvements);
            _db.SaveChanges();
        }

        public void EditExpenses(IEnumerable<Expense> expenses)
        {
            _db.UpdateRange(expenses);
            _db.SaveChanges();

            foreach (var expense in expenses) _db.Entry(expense).State = EntityState.Detached;
        }

        public void DeleteExpenses(IEnumerable<Expense> expenses)
        {
            _db.Expense.RemoveRange(expenses);
            _db.SaveChanges();
        }
    }
}
