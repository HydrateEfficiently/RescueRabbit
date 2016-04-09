using Microsoft.Data.Entity;
using RescueRabbit.Initializer.Initializers.Identity;
using RescueRabbit.Initializer.Initializers.Support;
using RescueRabbit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Initializer.Initializers
{
    public class MainDataInitializer : IDataInitializer
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly SystemUserInitializer _systemUserInitializer;
        private readonly SupportOutletInitializer _supportOutletInitializer;
        private readonly IEnvironmentInitializer _environmentInitializer;

        public MainDataInitializer(
            ApplicationDbContext dbContext,
            SystemUserInitializer systemUserInitializer,
            SupportOutletInitializer supportOutletInitializer,
            IEnvironmentInitializer environmentInitializer)
        {
            _dbContext = dbContext;
            _systemUserInitializer = systemUserInitializer;
            _supportOutletInitializer = supportOutletInitializer;
            _environmentInitializer = environmentInitializer;
        }

        public void Run()
        {
            _dbContext.Database.Migrate();

            // Identity
            _systemUserInitializer.Run();

            // Support
            _supportOutletInitializer.Run();

            _environmentInitializer.Run();
        }
    }
}
