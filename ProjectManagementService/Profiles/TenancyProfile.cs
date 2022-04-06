using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using TenancyEntity = PropertyManagementData.Model.Tenancy;

namespace PropertyManagementService.Profiles
{
    public class TenancyProfile : Profile
    {
        public TenancyProfile()
        {
            CreateMap<TenancyEntity, Tenancy>();

            CreateMap<Tenancy, TenancyEntity>()
                .ForMember(t => t.AgentId, opt => opt.Ignore())
                .ForMember(t => t.Agent, opt => opt.Ignore());
        }
    }
}
