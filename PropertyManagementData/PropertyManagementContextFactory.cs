using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagementData
{
    public class PropertyManagementContextFactory : IDesignTimeDbContextFactory<PropertyManagementContext>
    {
        public PropertyManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PropertyManagementContext>()
                .UseLazyLoadingProxies()
                .UseSqlite(@"Data Source=C:\Users\fazal\source\repos\PropertyManagementSystem\PropertyManagementData\PropertyManagement.db");

            return new PropertyManagementContext(optionsBuilder.Options);
        }
    }
}
