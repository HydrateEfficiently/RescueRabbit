using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Controllers
{
    [Route("support")]
    public class SupportController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("service-directory")]
        public IActionResult ServiceDirectory()
        {
            return View();
        }
    }
}
