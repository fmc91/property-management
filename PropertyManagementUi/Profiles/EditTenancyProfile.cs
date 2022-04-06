using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi.Profiles
{
    public class EditTenancyProfile : Profile
    {
        public EditTenancyProfile()
        {
            CreateMap<Tenancy, EditTenancyViewModel>();

            CreateMap<EditTenancyViewModel, Tenancy>()
                .ForMember(t => t.AccountEntries, opt => opt.Ignore());
        }
    }
}
