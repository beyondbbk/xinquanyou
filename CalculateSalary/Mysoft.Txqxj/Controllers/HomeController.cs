using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        //依赖注入
        public IHostingEnvironment _host;
        public HomeController(IHostingEnvironment _host)
        {
            this._host = _host;
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

            using (var fs = System.IO.File.Create(Path.Combine(_host.WebRootPath, "tjqxj","uploadimg", file.FileName)))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return new ClientResult(ResultDto.DefaultSuccess("success"));

        }
    }
}
