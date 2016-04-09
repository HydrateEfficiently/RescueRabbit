using AutoMapper;
using AutoMapper.Mappers;
using RescueRabbit.Framework.Models.Motivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Motivation.Models
{
    public class ReadMotivationPiece
    {
        private static MappingEngine __mappingEngine;

        static ReadMotivationPiece()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            var mappingEngine = new MappingEngine(configuration);
            configuration.CreateMap<MotivationPiece, ReadMotivationPiece>();
            __mappingEngine = mappingEngine;
        }

        public Guid Id { get; set; }

        public Guid MotivationBoardId { get; set; }

        public int Order { get; set; }

        public string Description { get; set; }

        public ReadMotivationPiece(MotivationPiece piece)
        {
            __mappingEngine.Map(piece, this);
        }
    }
}
