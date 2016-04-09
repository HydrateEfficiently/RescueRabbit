using AutoMapper;
using AutoMapper.Mappers;
using RescueRabbit.Framework.Models.Motivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Motivation.Models
{
    public class CreateMotivationPiece
    {
        private static MappingEngine __mappingEngine;

        static CreateMotivationPiece()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            var mappingEngine = new MappingEngine(configuration);
            configuration.CreateMap<CreateMotivationPiece, MotivationPiece>();
            __mappingEngine = mappingEngine;
        }

        public string Description { get; set; }

        public MotivationPiece Map()
        {
            return __mappingEngine.Map<CreateMotivationPiece, MotivationPiece>(this);
        }
    }
}
