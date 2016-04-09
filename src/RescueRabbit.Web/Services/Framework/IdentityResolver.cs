using Microsoft.AspNet.Http;
using RescueRabbit.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Services.Framework
{
    public class IdentityResolver : IIdentityResolver
    {

        private readonly IHttpContextAccessor _contextAccessor;

        public IdentityResolver(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal Resolve()
        {
            if (_contextAccessor == null || _contextAccessor.HttpContext == null)
            {
                return ClaimsPrincipal.Current;
            }
            return _contextAccessor.HttpContext.User;
        }

        public bool IsSignedIn()
        {
            var user = Resolve();
            return user != null && user.IsSignedIn();
        }
    }
}
