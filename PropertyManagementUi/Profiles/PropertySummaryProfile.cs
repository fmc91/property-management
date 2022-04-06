using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi.Profiles
{
    public class PropertySummaryProfile : Profile
    {
        public PropertySummaryProfile()
        {
            CreateMap<Property, PropertySummaryViewModel>()
                .ForMember(vm => vm.Tenants, opt => opt.MapFrom(src => src.Tenancy.Tenants))
                .ForMember(vm => vm.OutstandingBalance, opt => opt.MapFrom(src => src.Tenancy.OutstandingBalance))
                .ForMember(vm => vm.NextPaymentDate, opt => opt.MapFrom(src => src.Tenancy.NextPaymentDate))
                .ForMember(vm => vm.InsurancePolicyEndDate, opt => opt.MapFrom(src => src.InsurancePolicy.EndDate))
                .ForMember(vm => vm.ElectricalInspectionCertificateExpiry, opt => opt.MapFrom(src => src.ElectricalInspectionCertificate.ExpiryDate))
                .ForMember(vm => vm.GasSafetyCertificateExpiry, opt => opt.MapFrom(src => src.GasSafetyCertificate.ExpiryDate))
                .ForMember(vm => vm.EnergyPerformanceCertificateExpiry, opt => opt.MapFrom(src => src.EnergyPerformanceCertificate.ExpiryDate));
        }
    }
}
