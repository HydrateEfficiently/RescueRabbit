using Microsoft.AspNet.Identity;
using RescueRabbit.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Services.Identity
{
    public interface IAccountService
    {
        Task Login(LoginRequest request);

        Task Logout();
    }

    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Email);
            if (user == null)
            {
                throw new IdentityErrorException("Invalid login attempt.");
            }

            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isEmailConfirmed)
            {
                throw new IdentityErrorException("You must have a confirmed email to log in.");
            }

            var result = await _signInManager.PasswordSignInAsync(
                request.Email, request.Password, request.RememberMe, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                throw new IdentityErrorException("User account locked out.");
            }
            else if (!result.Succeeded)
            {
                throw new IdentityErrorException("Invalid login attempt.");
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
