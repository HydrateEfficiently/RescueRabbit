using RescueRabbit.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Framework.Utility
{
    public static class IdentityResolverExtensions
    {
        public static string GetId(
            this IIdentityResolver identityResolver)
        {
            return identityResolver.Resolve().GetId();
        }
    }
}
