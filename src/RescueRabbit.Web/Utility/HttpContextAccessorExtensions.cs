using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Utility
{
    public static class HttpContextAccessorExtensions
    {
        public static IUrlHelper GetUrlHelper(
            this IHttpContextAccessor httpContextAccessor)
        {
            return GetRequestService<IUrlHelper>(httpContextAccessor);
        }

        public static IJsonHelper GetJsonHelper(
            this IHttpContextAccessor httpContextAccessor)
        {
            return GetRequestService<IJsonHelper>(httpContextAccessor);
        }

        #region Helpers

        private static TService GetRequestService<TService>(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var httpRequestServices = httpContextAccessor.HttpContext.RequestServices;
            return httpRequestServices.GetRequiredService<TService>();
        }

        #endregion
    }
}
