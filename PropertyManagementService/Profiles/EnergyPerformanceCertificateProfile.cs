using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using EnergyPerformanceCertificateEntity = PropertyManagementData.Model.EnergyPerformanceCertificate;

namespace PropertyManagementService.Profiles
{
    public class EnergyPerformanceCertificateProfile : Profile
    {
        public EnergyPerformanceCertificateProfile()
        {
            CreateMap<EnergyPerformanceCertificateEntity, EnergyPerformanceCertificate>();

            CreateMap<EnergyPerformanceCertificate, EnergyPerformanceCertificateEntity>()
                .ForMember(c => c.PropertyId, opt => opt.Ignore());
        }
    }
}
