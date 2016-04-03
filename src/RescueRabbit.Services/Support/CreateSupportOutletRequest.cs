using AutoMapper;
using AutoMapper.Mappers;
using RescueRabbit.Framework.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Support
{
    public class CreateSupportOutletRequest
    {
        private static MappingEngine __mappingEngine;

        static CreateSupportOutletRequest()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            var mappingEngine = new MappingEngine(configuration);
            configuration.CreateMap<CreateSupportOutletRequest, SupportOutlet>();
            __mappingEngine = mappingEngine;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public SupportOutlet ToSupportOutlet()
        {
            return __mappingEngine.Map<CreateSupportOutletRequest, SupportOutlet>(this);
        }
    }
}
