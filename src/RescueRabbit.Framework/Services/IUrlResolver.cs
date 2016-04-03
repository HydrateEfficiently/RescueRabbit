using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Framework.Services
{
    public interface IUrlResolver
    {
        string ResolveConfirmationEmailUrl(string userId, string emailConfirmationToken);
    }
}
