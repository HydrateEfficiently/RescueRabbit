using RescueRabbit.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Framework.Models.Support
{
    public class SupportOutlet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public DateTime Created { get; set; }

        public string CreatedById { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }
    }
}
