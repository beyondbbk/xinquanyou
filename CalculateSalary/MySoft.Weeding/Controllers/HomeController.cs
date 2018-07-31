using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySoft.Weeding.Models;

namespace MySoft.Weeding.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sleep()
        {
            Thread.Sleep(10000);
            return Content("Finished");
        }
    }
}
