using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using AccountEntryEntity = PropertyManagementData.Model.AccountEntry;

namespace PropertyManagementService.Profiles
{
    public class AccountEntryProfile : Profile
    {
        public AccountEntryProfile()
        {
            CreateMap<AccountEntryEntity, AccountEntry>();

            CreateMap<AccountEntry, AccountEntryEntity>()
                .ForMember(e => e.TenancyId, opt => opt.Ignore());
        }
    }
}
