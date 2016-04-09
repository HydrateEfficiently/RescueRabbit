using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RescueRabbit.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Services.Framework
{
    public class UrlResolver : IUrlResolver
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ResolveConfirmationEmailUrl(string userId, string code)
        {
            return GetUrl("ConfirmEmail", "Registration", new { userId = userId, code = code });
        }

        #region Helpers

        private string GetUrl(string action, string controller, object values)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var httpRequestServices = _httpContextAccessor.HttpContext.RequestServices;
            var urlHelper = httpRequestServices.GetRequiredService<IUrlHelper>();
            string url = urlHelper.Action(action, controller, values, httpContext.Request.Scheme);
            return url;
        }

        #endregion
    }
}
