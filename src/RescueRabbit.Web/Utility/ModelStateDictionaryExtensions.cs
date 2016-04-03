using Microsoft.AspNet.Mvc.ModelBinding;
using RescueRabbit.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Utility
{
    public static class ModelStateDictionaryExtensions
    {
        public static void AddIdentityErrors(
            this ModelStateDictionary modelState,
            IdentityErrorException ex)
        {
            foreach (string error in ex.Errors)
            {
                modelState.AddModelError(string.Empty, error);
            }
        }
    }
}
