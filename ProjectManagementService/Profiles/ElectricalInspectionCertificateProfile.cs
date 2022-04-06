using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PropertyManagementService.Model;
using ElectricalInspectionCertificateEntity = PropertyManagementData.Model.ElectricalInspectionCertificate;

namespace PropertyManagementService.Profiles
{
    public class ElectricalInspectionCertificateProfile : Profile
    {
        public ElectricalInspectionCertificateProfile()
        {
            CreateMap<ElectricalInspectionCertificateEntity, ElectricalInspectionCertificate>();

            CreateMap<ElectricalInspectionCertificate, ElectricalInspectionCertificateEntity>()
                .ForMember(c => c.PropertyId, opt => opt.Ignore());
        }
    }
}
