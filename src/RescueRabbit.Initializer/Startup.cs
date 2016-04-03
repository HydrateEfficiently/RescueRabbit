using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using RescueRabbit.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using RescueRabbit.Initializer.Services;
using RescueRabbit.Framework.Services;
using RescueRabbit.Initializer.Initializers;
using RescueRabbit.Initializer.Initializers.Identity;
using RescueRabbit.Initializer.Services.DataImportServices;

namespace RescueRabbit.Initializer
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment hostingEnv)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnv.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices(connectionString: Configuration["Data:DefaultConnection:ConnectionString"]);

            // Framework Services
            services.AddTransient<IEmailService, StubbedEmailService>();
            services.AddTransient<IIdentityResolver, SystemUserIdentityResolver>();

            // Services
            services.AddTransient<JsonDataImportService>();

            // Main
            services.AddTransient<MainDataInitializer>();

            // Environments
            services.AddTransient<IEnvironmentInitializer, DebugEnvironmentInitializer>();

            // Components
            services.AddTransient<SystemUserInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, MainDataInitializer dataInitializer)
        {
            dataInitializer.Run();
            Environment.Exit(0);
        }

        // Entry point for the application.
        public static void Main(string[] args) { }
    }
}
