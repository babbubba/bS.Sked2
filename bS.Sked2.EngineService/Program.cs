using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace bS.Sked2.EngineService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureServices(services =>
            {
                services.Configure<EventLogSettings>(config =>
                {
                    config.LogName = "Engine Service";
                    config.SourceName = "Engine Service Source";
                });
                services.AddHostedService<Scheduler>();
            })
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureWebHost(config =>
                {
                    config.UseUrls("https://*:17601");
                })
            .UseWindowsService();
    }
}
