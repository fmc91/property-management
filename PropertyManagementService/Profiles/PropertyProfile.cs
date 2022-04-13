using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyEntity = PropertyManagementData.Model.Property;

namespace PropertyManagementService.Profiles
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<PropertyEntity, Property>()
                .ForMember(p => p.ElectricalInspectionCertificate, opt => opt.MapFrom(p => p.ElectricalInspectionCertificates
                    .Where(c => c.IssueDate <= DateTime.Today)
                    .OrderByDescending(c => c.ExpiryDate)
                    .FirstOrDefault()))
                .ForMember(p => p.GasSafetyCertificate, opt => opt.MapFrom(p => p.GasSafetyCertificates
                    .Where(c => c.IssueDate <= DateTime.Today)
                    .OrderByDescending(c => c.ExpiryDate)
                    .FirstOrDefault()))
                .ForMember(p => p.EnergyPerformanceCertificate, opt => opt.MapFrom(p => p.EnergyPerformanceCertificates
                    .Where(c => c.IssueDate <= DateTime.Today)
                    .OrderByDescending(c => c.ExpiryDate)
                    .FirstOrDefault()))
                .ForMember(p => p.InsurancePolicy, opt => opt.MapFrom(p => p.InsurancePolicies
                    .Where(i => i.StartDate <= DateTime.Today)
                    .OrderByDescending(i => i.EndDate)
                    .FirstOrDefault()))
                .ForMember(p => p.Tenancy, opt => opt.MapFrom(p => p.Tenancies
                    .Where(t => t.StartDate <= DateTime.Today && t.EndDate >= DateTime.Today)
                    .FirstOrDefault()));

            CreateMap<Property, PropertyEntity>()
                .ForMember(p => p.Owner, opt => opt.Ignore())
                .ForMember(p => p.OwnerId, opt => opt.Ignore())
                .ForMember(p => p.ElectricalInspectionCertificates, opt => opt.Ignore())
                .ForMember(p => p.GasSafetyCertificates, opt => opt.Ignore())
                .ForMember(p => p.EnergyPerformanceCertificates, opt => opt.Ignore())
                .ForMember(p => p.InsurancePolicies, opt => opt.Ignore())
                .ForMember(p => p.Tenancies, opt => opt.Ignore())
                .ForMember(p => p.Improvements, opt => opt.Ignore())
                .ForMember(p => p.Expenses, opt => opt.Ignore());
        }
    }
}
