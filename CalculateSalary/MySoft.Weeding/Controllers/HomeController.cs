using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySoft.Weeding.Models;
using MySoft.Weeding.Services;

namespace MySoft.Weeding.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetStrArr(GetStrVm getStrVm)
        {
            //var buffer = new byte[Convert.ToInt32(Request.ContentLength)];
            ////if (buffer.Length != 0) Request.Body.Position = 0;
            //Request.Body.Read(buffer, 0, buffer.Length);
            //var json = Encoding.UTF8.GetString(buffer);
            var result = CreateStrArray.Get(getStrVm.Text, getStrVm.FontSize,getStrVm.FontName);
            return Json(new {status = 0, data = result});
        }
    }
}
