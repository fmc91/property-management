using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using InsurancePolicyEntity = PropertyManagementData.Model.InsurancePolicy;

namespace PropertyManagementService.Profiles
{
    public class InsurancePolicyProfile : Profile
    {
        public InsurancePolicyProfile()
        {
            CreateMap<InsurancePolicyEntity, InsurancePolicy>();

            CreateMap<InsurancePolicy, InsurancePolicyEntity>()
                .ForMember(p => p.BrokerId, opt => opt.Ignore())
                .ForMember(p => p.InsurerId, opt => opt.Ignore());
        }
    }
}
