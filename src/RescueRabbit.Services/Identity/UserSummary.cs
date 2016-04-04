using AutoMapper;
using AutoMapper.Mappers;
using RescueRabbit.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Identity
{
    public class UserSummary
    {
        private static MappingEngine __mappingEngine;

        static UserSummary()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            var mappingEngine = new MappingEngine(configuration);
            configuration.CreateMap<ApplicationUser, UserSummary>();
            __mappingEngine = mappingEngine;
        }

        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CareFacility { get; set; }

        public UserSummary(ApplicationUser user)
        {
            __mappingEngine.Map(user, this);
        }
    }
}
