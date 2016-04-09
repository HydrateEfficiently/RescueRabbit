using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RescueRabbit.Web.Controllers
{
    [Route("motivation")]
    public class MotivationController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
