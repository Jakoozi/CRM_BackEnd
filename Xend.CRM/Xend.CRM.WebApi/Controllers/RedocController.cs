using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Xend.CRM.WebApi.Controllers
{
    public class RedocController : Controller
    {
        // GET: /<controller>/

        [HttpGet("redoc")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
