using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi.Profiles
{
    public class PropertyDetailsProfile : Profile
    {
        public PropertyDetailsProfile()
        {
            CreateMap<Property, PropertyDetailsViewModel>()
                .ForMember(vm => vm.ImageId, opt => opt.MapFrom(src => src.Image.ImageId))
                .ForMember(vm => vm.ImagePath, opt => opt.Ignore())
                .ForMember(vm => vm.NewInsurancePolicy, opt => opt.Ignore())
                .ForMember(vm => vm.NewElectricalInspectionCertificate, opt => opt.Ignore())
                .ForMember(vm => vm.NewGasSafetyCertificate, opt => opt.Ignore())
                .ForMember(vm => vm.NewEnergyPerformanceCertificate, opt => opt.Ignore())
                .ForMember(vm => vm.NewExpense, opt => opt.Ignore())
                .ForMember(vm => vm.NewImprovement, opt => opt.Ignore())
                .ForMember(vm => vm.ExpensesForEdit, opt => opt.Ignore())
                .ForMember(vm => vm.ImprovementsForEdit, opt => opt.Ignore());
        }
    }
}
