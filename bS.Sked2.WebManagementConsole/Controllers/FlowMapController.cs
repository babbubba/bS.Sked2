using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace bS.Sked2.WebManagementConsole.Controllers
{
    public class FlowMapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}