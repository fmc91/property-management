using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using PropertyManagementUi.ViewModels;

namespace PropertyManagementUi.Profiles
{
    public class NewInsurancePolicyProfile : Profile
    {
        public NewInsurancePolicyProfile()
        {
            CreateMap<NewInsurancePolicyViewModel, InsurancePolicy>()
                .ForMember(p => p.InsurancePolicyId, opt => opt.Ignore());
        }
    }
}
