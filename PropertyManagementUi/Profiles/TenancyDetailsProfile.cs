using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi.Profiles
{
    public class TenancyDetailsProfile : Profile
    {
        public TenancyDetailsProfile()
        {
            CreateMap<Tenancy, TenancyDetailsViewModel>()
                .ForMember(vm => vm.StreetAddress1, opt => opt.MapFrom(src => src.Property.StreetAddress1))
                .ForMember(vm => vm.StreetAddress2, opt => opt.MapFrom(src => src.Property.StreetAddress2))
                .ForMember(vm => vm.Locality, opt => opt.MapFrom(src => src.Property.Locality))
                .ForMember(vm => vm.City, opt => opt.MapFrom(src => src.Property.City))
                .ForMember(vm => vm.County, opt => opt.MapFrom(src => src.Property.County))
                .ForMember(vm => vm.Country, opt => opt.MapFrom(src => src.Property.Country))
                .ForMember(vm => vm.Postcode, opt => opt.MapFrom(src => src.Property.Postcode));
        }
    }
}
