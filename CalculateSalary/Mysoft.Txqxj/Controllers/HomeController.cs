using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mysoft.Tjqxj.Models.ViewModel;
using Mysoft.Tjqxj.Services;
using Mysoft.Txqxj.API.Filters;
using Mysoft.Txqxj.API.Models.Respond;
using Mysoft.Txqxj.Models;
using Mysoft.Txqxj.Models.Home;
using Mysoft.Txqxj.Models.Result;

namespace Mysoft.Txqxj.Controllers
{
    [ModelVerify]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerifyWx()
        {
            var timestamp = HttpContext.Request.Query["timestamp"];
            var sign = HttpContext.Request.Query["signature"];
            var echostr = HttpContext.Request.Query["echostr"];
            var nonce = HttpContext.Request.Query["nonce"];

            if (HomeService.VerifyWx(timestamp,nonce,sign)) return new TextResult(echostr);
            return new TextResult("签名错误");
        }

        public IActionResult FeedBack()
        {
            var requestUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            var signVm = WxCommonService.GetJsSignVm(requestUrl);
            return View(signVm);
        }

        public IActionResult Upload()
        {
            var file = Request.Form.Files[0];
            var json = Request.Form["json"];

            using (var fs = System.IO.File.Create(@"c:/1.jpeg"))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return Json(new { });
  
        }
    }
}
