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
        private static object ob = new object();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetWordArr(GetStrVm getStrVm)
        {
 
            lock (ob)
            {
                var result = CreateStrArray.GetFormText(getStrVm.Text, getStrVm.FontSize, getStrVm.FontName);
                return new ClientResult(ResultDto.DefaultSuccess(result));
            }
        }


        public IActionResult GetImgArr(GetImgVm getImgVm)
        {
            lock (ob)
            {
                var result = CreateStrArray.GetFormBmp(getImgVm.ImgName,getImgVm.Width,getImgVm.Height);
                return new ClientResult(ResultDto.DefaultSuccess(result));
            }
        }

        public IActionResult test()
        {
            return Content("success");
        }
    }
}
