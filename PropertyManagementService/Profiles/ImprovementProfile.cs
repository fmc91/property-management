using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using ImprovementEntity = PropertyManagementData.Model.Improvement;

namespace PropertyManagementService.Profiles
{
    public class ImprovementProfile : Profile
    {
        public ImprovementProfile()
        {
            CreateMap<ImprovementEntity, Improvement>();

            CreateMap<Improvement, ImprovementEntity>()
                .ForMember(i => i.PropertyId, opt => opt.Ignore());
        }
    }
}
