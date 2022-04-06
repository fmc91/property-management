using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyManagementUi.Profiles
{
    public class AddTenancyProfile : Profile
    {
        public AddTenancyProfile()
        {
            CreateMap<AddTenancyViewModel, Tenancy>()
                .ForMember(t => t.AccountEntries, opt => opt.MapFrom(src => new List<AccountEntry>()))
                .ForMember(t => t.ScheduledPayments, opt => opt.MapFrom(src => new List<ScheduledPayment>()))
                .ForMember(t => t.Property, opt => opt.Ignore());
        }
    }
}
