using RescueRabbit.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Framework.Models.Motivation
{
    public class MotivationPiece
    {
        public Guid Id { get; set; }

        public Guid MotivationBoardId { get; set; }
        
        public virtual MotivationBoard MotivationBoard { get; set; }

        public int Order { get; set; }

        public string Description { get; set; }
    }
}
