using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using RateEntity = PropertyManagementData.Model.Rate;

namespace PropertyManagementService.Profiles
{
    public class RateProfile : Profile
    {
        public RateProfile()
        {
            CreateMap<RateEntity, Rate>();

            CreateMap<Rate, RateEntity>()
                .ForMember(r => r.TenancyId, opt => opt.Ignore());
        }
    }
}
