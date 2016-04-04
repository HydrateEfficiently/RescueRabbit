using Microsoft.Data.Entity.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Framework.Utility
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder HasTableName(
            this EntityTypeBuilder builder,
            string tableName)
        {
            builder.HasAnnotation("Relational:TableName", tableName);
            return builder;
        }
    }
}
