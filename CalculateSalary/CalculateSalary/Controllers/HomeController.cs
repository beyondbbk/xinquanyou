using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CalculateSalary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MySoft.CalculateSalary.Services;
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
            return View();
        }

        //保存上传的文件
        public IActionResult Upload()
        {
            var time = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            var files = Request.Form.Files;
            
            foreach (var file in files)
            {
                LogHelper.Debug("收到文件上传：" + file.FileName);
                var filename = file.FileName.Trim('"');
                var pathname = Path.Combine(_host.WebRootPath, "xqy", "temp", time);
                //var pathname = $"{_host.WebRootPath}\\xqy\\temp\\{time}";
                if (!Directory.Exists(pathname)) Directory.CreateDirectory(pathname);

                using (var fs = System.IO.File.Create(Path.Combine(pathname, filename)))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            //将time作为cookie返回，下次解析时将以此为凭据
            Response.Cookies.Append("time", time);
            return Json(new{});
        }

        public IActionResult CalFile()
        {
            var time= Request.Cookies["time"];
            if (string.IsNullOrEmpty(time))
            {
                ViewData["Message"] = "请先上传文件";
                return View("Error");
            }
            var pathname = Path.Combine(_host.WebRootPath, "xqy", "temp", time);
            if (!Directory.Exists(pathname))
            {
                ViewData["Message"] = "文件已经被清理，请重新上传";
                return View("Error");
            }
            var vmList = new List<CalFileViewModel>();
            var files = Directory.GetFiles(pathname, "*.xlsx");
            foreach (var file in files)
            {
                var calFileViewModel = new CalFileViewModel();
                calFileViewModel.ExcelName = Path.GetFileName(file);
                var resultDto = CalFileService.GetCalResult(file);
                calFileViewModel.IsSuccess = resultDto.IsSuccess;
                calFileViewModel.ErrMsg = resultDto.ErrMsg;
                calFileViewModel.Msg = resultDto.Msg;
                calFileViewModel.TeacherDetails = resultDto.TeacherDetails;
                vmList.Add(calFileViewModel);
            }
            return View(vmList);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
