using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MySoft.Common;
using MySoft.ResManger.Filters;
using MySoft.ResManger.Models;
using MySoft.ResManger.Services;
using Newtonsoft.Json;

namespace MySoft.ResManger.Controllers
{
    [ModelFilter]
    public class HomeController : Controller
    {
        //依赖注入
        public IHostingEnvironment _host;
        public HomeController(IHostingEnvironment _host)
        {
            this._host = _host;
        }

        public IActionResult AnalyExcelFile(string companyName, string sheetName, int pageNum,string searchWords)
        {
            var excelpath = Path.Combine(_host.WebRootPath, "excel", companyName+".xlsx");
            if (pageNum == 0) pageNum = 1;
            return View(AnalyExcelService.GetView(excelpath,sheetName,pageNum, searchWords));
        }

        public IActionResult SetExcelValue(string companyName,string sheetName,int row,int col,string newData)
        {
            var excelpath = Path.Combine(_host.WebRootPath, "excel", companyName + ".xlsx");
            AnalyExcelService.SetCellValue(excelpath, sheetName, row, col, newData);
            return Json(new Result() {IsSuccess = true});
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
            var pathname = Path.Combine(_host.WebRootPath, "excel");
            if (!Directory.Exists(pathname)) Directory.CreateDirectory(pathname);

            using (var fs = System.IO.File.Create(Path.Combine(pathname, companyName+".xlsx")))
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

        public IActionResult Test(TestModel test)
        {
            return Content("Finished");
        }
    }

    public class TestModel
    {
        [Required(ErrorMessage = "Name必须")]
        public string Name { get; set; }
    }
}
