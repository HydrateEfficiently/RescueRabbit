using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using RescueRabbit.Framework.Models.Identity;
using RescueRabbit.Services.Identity;
using RescueRabbit.Services.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services
{
    public static class ServiceCollectionExtensions
    {

        public static void AddServices(
            this IServiceCollection services,
            string connectionString,
            string loginPath = null)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                if (loginPath != null)
                {
                    options.Cookies.ApplicationCookie.LoginPath = loginPath;
                }
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Identity
            services.AddScoped<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IAccountService, AccountService>();

            // Support
            services.AddTransient<ISupportService, SupportService>();
        }
    }
}
