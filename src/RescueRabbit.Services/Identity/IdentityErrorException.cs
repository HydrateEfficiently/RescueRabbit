using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Identity
{
    public class IdentityErrorException : Exception
    {
        public IEnumerable<string> Errors { get; set; }

        public IdentityErrorException(params string[] errors) : base()
        {
            Errors = errors;
        }
    }
}
