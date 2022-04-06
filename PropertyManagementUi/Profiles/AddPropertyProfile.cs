using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementUi.Profiles
{
    public class AddPropertyProfile : Profile
    {
        public AddPropertyProfile()
        {
            CreateMap<AddPropertyViewModel, Property>()
                .ForMember(p => p.Image, opt =>
                {
                    opt.PreCondition(vm => vm.ImageId != null);
                    opt.MapFrom(src => new Image { ImageId = src.ImageId.Value });
                })
                .ForMember(p => p.Tenancy, opt => opt.Ignore())
                .ForMember(p => p.Improvements, opt => opt.Ignore())
                .ForMember(p => p.Expenses, opt => opt.Ignore())
                .ForMember(p => p.InsurancePolicy, opt => opt.Ignore())
                .ForMember(p => p.ElectricalInspectionCertificate, opt => opt.Ignore())
                .ForMember(p => p.GasSafetyCertificate, opt => opt.Ignore())
                .ForMember(p => p.EnergyPerformanceCertificate, opt => opt.Ignore());
        }
    }
}
