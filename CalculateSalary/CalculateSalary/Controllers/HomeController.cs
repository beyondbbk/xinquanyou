using System;
using System.Collections.Generic;
using System.IO;
using CalculateSalary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MySoft.Common;

namespace MySoft.CalculateSalary.Controllers
{
    public class HomeController : Controller
    {
        //依赖注入
        public IHostingEnvironment _host;
        public HomeController(IHostingEnvironment _host)
        {
            this._host = _host;
        }
        public IActionResult Index()
        {
            MyLogHelper.Debug("342");
            return View();
        }

        //保存上传的文件
        public IActionResult Upload()
        {
            var time = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                var filename = file.FileName.Trim('"');
                var pathname = $"{_host.WebRootPath}\\xqy\\temp\\{time}";
                if (!Directory.Exists(pathname)) Directory.CreateDirectory(pathname);

                using (FileStream fs = System.IO.File.Create(pathname+"//"+filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            //将guid作为cookie返回，下次解析时将以此为凭据
            Response.Cookies.Append("FileId", time);
            return Json(new{});
        }

        public IActionResult CalFile()
        {
            var fileId= Request.Cookies["FileId"];
            if (string.IsNullOrEmpty(fileId))
            {
                ViewData["Message"] = "请先上传文件";
                return View("Error");
            }
            var pathname = $"{_host.WebRootPath}\\xqy\\temp\\{fileId}";
            if (!Directory.Exists(pathname))
            {
                ViewData["Message"] = "文件已经被清理，请重新上传";
                return View("Error");
            }
            var result = new CalFileViewModel();
   
            result.Results.Add("test.xlsx",new List<WorkerInfo>
            {
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangj321ian",Salary = 4321.7},
                new WorkerInfo(){Name = "yanjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "ygjian",Salary = 4.7},
                new WorkerInfo(){Name = "yajian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yanian",Salary = 4.7},
                new WorkerInfo(){Name = "yang90ian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangj0980ian",Salary = 4.7},
                new WorkerInfo(){Name = "ya0980ngjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4098.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4098.7},
                new WorkerInfo(){Name = "yangjian",Salary = 4.7},new WorkerInfo(){Name = "yang0980890jian",Salary = 4098.7},

            });
            return View(result);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
