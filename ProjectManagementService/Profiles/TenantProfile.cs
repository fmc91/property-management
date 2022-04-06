using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using TenantEntity = PropertyManagementData.Model.Tenant;

namespace PropertyManagementService.Profiles
{
    public class TenantProfile : Profile
    {
        public TenantProfile()
        {
            CreateMap<TenantEntity, Tenant>();

            CreateMap<Tenant, TenantEntity>()
                .ForMember(t => t.TenancyId, opt => opt.Ignore());
        }
    }
}
