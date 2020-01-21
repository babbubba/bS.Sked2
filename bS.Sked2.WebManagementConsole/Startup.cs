using bs.Data;
using bs.Data.Interfaces;
using bS.Sked2.Engine;
using bS.Sked2.Model.Engine;
using bS.Sked2.Model.Repositories;
using bS.Sked2.Model.Service;
using bS.Sked2.Service.UI;
using bS.Sked2.Structure.Engine;
using bS.Sked2.Structure.Repositories;
using bS.Sked2.Structure.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace bS.Sked2.WebManagementConsole
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            var dbContext = new DbContext
            {
                ConnectionString = "Data Source=.\\bs.Data.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = "sqlite",
                Create = false,
                Update = true,
                UseExecutingAssemblyToo = false,
                LookForEntitiesDllInCurrentDirectoryToo = false,
                FoldersWhereLookingForEntitiesDll = new string[] { @"..\bS.Sked2.Extensions.Common\bin\Debug\netcoreapp3.0\" },
                EntitiesFileNameScannerPatterns = new string[] { "bS.Sked2.Extensions.*.dll", "bS.Sked2.Model.dll" }
            };

            var engineUiServiceConfigx = new EngineUIServiceConfig
            {
                ExtensionsFolder = @"..\bS.Sked2.Extensions.Common\bin\Debug\netcoreapp3.0\"
            };

            var engineConfig = new EngineConfig
            {
                ExtensionsFolder = @"..\bS.Sked2.Extensions.Common\bin\Debug\netcoreapp3.0\"
            };

            // add logging
            services.AddLogging();

            //services.AddSingleton(Mock.Of<ILogger>());
            services.AddSingleton<IDbContext>(dbContext);
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEngineRepository, EngineRepository>();
            services.AddSingleton<IEngineConfig>(engineConfig);
            services.AddSingleton<IEngine, Engine.Engine>();
            services.AddSingleton<IEngineUIServiceConfig>(engineUiServiceConfigx);
            services.AddTransient<IEngineUIService, EngineUIService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            //app.UseCors(builder => builder.WithOrigins("https://localhost:44323"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
