using Microsoft.AspNet.Mvc;
using RescueRabbit.Framework.Services;
using RescueRabbit.Framework.Utility;
using RescueRabbit.Services.Motivation;
using RescueRabbit.Services.Motivation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Controllers.Api
{
    [Route("api/motivation")]
    public class MotivationApiController : Controller
    {
        private readonly IMotivationService _motivationService;
        private readonly IIdentityResolver _identityResolver;

        public MotivationApiController(
            IMotivationService motivationService,
            IIdentityResolver identityResolver)
        {
            _motivationService = motivationService;
            _identityResolver = identityResolver;
        }

        public async Task<IActionResult> CreateAsync([FromBody] CreateMotivationPiece model)
        {
            var result = await _motivationService.CreateAsync(model);
            return new ObjectResult(result);
        }

        public async Task<IActionResult> ListAsync()
        {
            var result = await _motivationService.ListAsync(_identityResolver.GetId());
            return new ObjectResult(result);
        }
    }
}
