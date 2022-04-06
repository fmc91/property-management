using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using PurchaseCostEntity = PropertyManagementData.Model.PurchaseCost;

namespace PropertyManagementService.Profiles
{
    public class PurchaseCostProfile : Profile
    {
        public PurchaseCostProfile()
        {
            CreateMap<PurchaseCostEntity, PurchaseCost>();

            CreateMap<PurchaseCost, PurchaseCostEntity>()
                .ForMember(e => e.PropertyId, opt => opt.Ignore());
        }
    }
}
