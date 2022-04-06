using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi.Profiles
{
    public class EditPropertyProfile : Profile
    {
        public EditPropertyProfile()
        {
            CreateMap<EditPropertyViewModel, Property>()
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

            CreateMap<Property, EditPropertyViewModel>()
                .ForMember(vm => vm.ImageId, opt => opt.MapFrom(src => src.Image.ImageId))
                .ForMember(vm => vm.ImagePath, opt => opt.Ignore())
                .ForMember(vm => vm.NewOwner, opt => opt.Ignore())
                .ForMember(vm => vm.Owners, opt => opt.Ignore());
        }
    }
}
