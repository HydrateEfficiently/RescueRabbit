using Microsoft.AspNet.Identity;
using RescueRabbit.Framework.Models.Identity;
using RescueRabbit.Framework.Services;
using RescueRabbit.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Identity
{
    public interface ICurrentUserProvider
    {
        Task<UserSummary> GetAsync();
    }

    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly IIdentityResolver _identityResolver;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserProvider(
            IIdentityResolver identityResolver,
            UserManager<ApplicationUser> userManager)
        {
            _identityResolver = identityResolver;
            _userManager = userManager;
        }

        public async Task<UserSummary> GetAsync()
        {
            var identity = _identityResolver.Resolve();
            var user = await _userManager.FindByIdAsync(identity.GetId());
            return new UserSummary(user);
        }
    }
}
