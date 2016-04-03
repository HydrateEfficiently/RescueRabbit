using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using RescueRabbit.Services.Identity;
using RescueRabbit.Web.Utility;
using RescueRabbit.Web.ViewModels.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Controllers
{
    [Route("account/register")]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(
            IRegistrationService registrationService,
            ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _registrationService.RegisterAsync(model.ToRegistrationRequest());
                    return RedirectToAction(nameof(Success));
                }
                catch (IdentityErrorException ex)
                {
                    ModelState.AddIdentityErrors(ex);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        [Route("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                await _registrationService.ConfirmEmailAsync(userId, code);
                return View();
            }
            catch (EmailConfirmationFailedException)
            {
                return RedirectToAction(nameof(ConfirmEmailFailed));
            }
        }

        [HttpGet]
        [Route("confirm-failed")]
        public IActionResult ConfirmEmailFailed(string userId, string code)
        {
            return View();
        }
    }
}
