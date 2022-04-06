using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PropertyManagementService.Model;
using ImageEntity = PropertyManagementData.Model.Image;

namespace PropertyManagementService.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageEntity>()
                .ForMember(i => i.PropertyId, opt => opt.Ignore())
                .ForMember(i => i.FileName, opt => opt.Ignore());

            CreateMap<ImageEntity, Image>();
        }
    }
}
