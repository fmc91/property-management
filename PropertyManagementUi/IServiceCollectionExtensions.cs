using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyManagementUi
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDelegate<TService, TDelegate>(this IServiceCollection services, Func<TService, TDelegate> resolve) where TDelegate : Delegate
        {
            return services.AddSingleton(serviceProvider => resolve(serviceProvider.GetRequiredService<TService>()));
        }
    }
}
