using Microsoft.AspNet.Identity;
using RescueRabbit.Framework.Models.Identity;
using RescueRabbit.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Identity
{
    public interface IRegistrationService
    {
        Task<ApplicationUser> RegisterAsync(RegistrationRequest request);

        Task SendConfirmationEmail(string userId);

        Task ConfirmEmailAsync(string userId, string code);
    }

    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUrlResolver _urlResolver;
        private readonly IEmailService _emailService;

        public RegistrationService(
            UserManager<ApplicationUser> userManager,
            IUrlResolver urlResolver,
            IEmailService emailService)
        {
            _userManager = userManager;
            _urlResolver = urlResolver;
            _emailService = emailService;
        }

        public async Task<ApplicationUser> RegisterAsync(RegistrationRequest request)
        {
            var user = request.ToApplicationUser();

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                throw new IdentityErrorException(createResult.Errors
                    .Select(e => e.Description)
                    .ToArray());
            }

            await SendConfirmationEmail(user);

            return user;
        }

        public async Task SendConfirmationEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await SendConfirmationEmail(user);
        }

        public async Task ConfirmEmailAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                throw new ArgumentNullException(nameof(userId), nameof(code));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"Could not find user with id {userId}");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new EmailConfirmationFailedException();
            }
        }

        #region Helpers

        private async Task SendConfirmationEmail(ApplicationUser user)
        {
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string emailConfirmationUrl = _urlResolver.ResolveConfirmationEmailUrl(user.Id, emailConfirmationToken);
            await _emailService.SendEmailAsync(
                user.Email,
                "Confirm your account with Rescue Rabbit",
                $"Please confirm your account by clicking this link: <a href='{emailConfirmationUrl}'>link</a>");
        }

        #endregion
    }
}
