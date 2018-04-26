using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MySoft.Common;
using MySoft.ResManger.Models;
using MySoft.ResManger.Services;

namespace MySoft.ResManger.Controllers
{
    public class HomeController : Controller
    {
        //依赖注入
        public IHostingEnvironment _host;
        public HomeController(IHostingEnvironment _host)
        {
            this._host = _host;
        }

        public IActionResult AnalyExcelFile(string companyName, string currentSheetName, int pageNum)
        {
            var excelpath = Path.Combine(_host.WebRootPath, "excel", "company", companyName,".xlsx");
            
            return View(AnalyExcelService.GetView(excelpath,currentSheetName,pageNum));
        }

        public IActionResult Index()
        {

            return View();
        }

        //保存上传的文件
        public IActionResult Upload(string companyName)
        {
            var file = Request.Form.Files[0];

            LogHelper.Debug("收到文件上传：" + file.FileName);
            var pathname = Path.Combine(_host.WebRootPath, "excel", "company");
            if (!Directory.Exists(pathname)) Directory.CreateDirectory(pathname);

            using (var fs = System.IO.File.Create(Path.Combine(pathname, companyName,".xlsx")))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return Json(new { });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
