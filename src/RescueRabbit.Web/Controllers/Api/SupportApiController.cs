using Microsoft.AspNet.Mvc;
using RescueRabbit.Services.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Controllers.Api
{
    [Route("api/support")]
    public class SupportApiController : Controller
    {
        private readonly ISupportService _supportService;

        public SupportApiController(
            ISupportService supportService)
        {
            _supportService = supportService;
        }

        public async Task<IActionResult> List()
        {
            var result = await _supportService.ListAsync();
            return new ObjectResult(result);
        }
    }
}
