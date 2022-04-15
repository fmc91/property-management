using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace PropertyManagementUi
{
    public static class IHostExtensions
    {
        public static void RunApplication(this IHost host)
        {
            host.Start();

            var app = host.Services.GetRequiredService<App>();
            app.Run();

            host.StopAsync().Wait();
        }
    }
}
