using RescueRabbit.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RescueRabbit.Framework.Utility
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ApplicationClaimTypes.Id);
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ApplicationClaimTypes.Email);
        }

        public static string GetFirstName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ApplicationClaimTypes.FirstName);
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ApplicationClaimTypes.UserName);
        }
    }
}
