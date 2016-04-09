using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json;
using RescueRabbit.Services;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using RescueRabbit.Framework.Services;
using Azn.RiskApp.Web.Services._Dev;
using RescueRabbit.Web.Services.ServerData;
using RescueRabbit.Web.Services.Framework;

namespace RescueRabbit.Web
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; set; }

        public string ConnectionString
        {
            get
            {
                return Configuration["Data:DefaultConnection:ConnectionString"];
            }
        }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureRouting(routeOptions =>
            {
                routeOptions.LowercaseUrls = true;
            });

            services.AddMvc(setup =>
            {
                //setup.Filters.Add(new RequireHttpsAttribute());
                setup.RespectBrowserAcceptHeader = true;

                setup.OutputFormatters.Insert(0, new JsonOutputFormatter()
                {
                    SerializerSettings = new JsonSerializerSettings() {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                });
                setup.OutputFormatters.Insert(1, new XmlSerializerOutputFormatter());
            });

            services.AddServices(connectionString: ConnectionString, loginPath: "/account/login");

            // Framework Services
            services.AddTransient<IIdentityResolver, IdentityResolver>();
            services.AddTransient<IEmailService, DevCsvEmailService>();
            services.AddTransient<IUrlResolver, UrlResolver>();

            // Local Services
            services.AddTransient<ServerDataBuilder>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Welcome}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
