using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using GasSafetyCertificateEntity = PropertyManagementData.Model.GasSafetyCertificate;

namespace PropertyManagementService.Profiles
{
    public class GasSafetyCertificateProfile : Profile
    {
        public GasSafetyCertificateProfile()
        {
            CreateMap<GasSafetyCertificateEntity, GasSafetyCertificate>();

            CreateMap<GasSafetyCertificate, GasSafetyCertificateEntity>()
                .ForMember(c => c.PropertyId, opt => opt.Ignore());
        }
    }
}
