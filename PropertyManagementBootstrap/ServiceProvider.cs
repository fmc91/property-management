using System;
using System.IO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PropertyManagementData;
using PropertyManagementService;
using PropertyManagementService.Model;
using PropertyManagementService.Profiles;
using Entities = PropertyManagementData.Model;

namespace PropertyManagementBootstrap
{
    public class ServiceProvider
    {
        private DbContextOptions<PropertyManagementContext> _dbContextOptions;

        private MapperConfiguration _mapperConfig;

        public ServiceProvider(DbContextOptions<PropertyManagementContext> dbContextOptions, MapperConfiguration mapperConfig)
        {
            _dbContextOptions = dbContextOptions;
            _mapperConfig = mapperConfig;
        }

        public PropertyService GetPropertyService()
        {
            return new PropertyService(new PropertyManagementContext(_dbContextOptions), new Mapper(_mapperConfig));
        }

        public ImageService GetImageService()
        {
            return new ImageService(new PropertyManagementContext(_dbContextOptions), Path.Combine(Directory.GetCurrentDirectory(), "property-img"));
        }
    }
}
