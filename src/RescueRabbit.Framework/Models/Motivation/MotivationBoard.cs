using RescueRabbit.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Framework.Models.Motivation
{
    public class MotivationBoard
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [InverseProperty(nameof(MotivationPiece.MotivationBoard))]
        public virtual ICollection<MotivationPiece> MotivationPieces { get; set; }
    }
}
