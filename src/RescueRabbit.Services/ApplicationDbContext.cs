using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using RescueRabbit.Framework.Models.Identity;
using Microsoft.Data.Entity;
using RescueRabbit.Framework.Models.Support;
using RescueRabbit.Framework.Utility;
using RescueRabbit.Framework.Models.Motivation;

namespace RescueRabbit.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<SupportOutlet> SupportOutlets { get; set; }

        public DbSet<MotivationBoard> MotivationBoards { get; set; }
        public DbSet<MotivationPiece> MotivationPieces { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SupportOutlet>().HasTableName(nameof(SupportOutlets));

            builder.Entity<MotivationBoard>().HasTableName(nameof(MotivationBoards));
            builder.Entity<MotivationPiece>().HasTableName(nameof(MotivationPieces));
        }
    }
}
