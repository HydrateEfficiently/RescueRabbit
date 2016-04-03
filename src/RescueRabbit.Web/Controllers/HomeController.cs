using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace RescueRabbit.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
