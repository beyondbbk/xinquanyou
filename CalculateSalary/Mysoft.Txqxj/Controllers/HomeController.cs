using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mysoft.Tjqxj.Models;
using Mysoft.Tjqxj.Models.Db;
using Mysoft.Tjqxj.Models.Request;
using Mysoft.Tjqxj.Models.Template;
using Mysoft.Tjqxj.Models.ViewModel;
using Mysoft.Tjqxj.Services;
using Mysoft.Txqxj.API.Filters;
using Mysoft.Txqxj.API.Models.Respond;
using Mysoft.Txqxj.Models;
using Mysoft.Txqxj.Models.Home;
using Mysoft.Txqxj.Models.Result;
using MySoft.Common;

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

            if (HomeService.VerifyWx(timestamp, nonce, sign)) return new TextResult(echostr);
            return new TextResult("签名错误");
        }

        public IActionResult FeedBack()
        {
            var requestUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            LogHelper.Info("请求feedback完整参数：" + requestUrl);
            var code = Request.Query.ContainsKey("CODE") ? Request.Query["CODE"].ToString() : "";
            var signVm = WxCommonService.GetJsSignVm(requestUrl);
            if (!string.IsNullOrEmpty(code))
            {
                signVm.OpenId = WxCommonService.GetOpenId(code);
            }

            return View(signVm);
        }

        public IActionResult Upload()
        {
            if (Request.Form.Files.Any())
            {
                //去重
                var file = Request.Form.Files[0];
                var fileId = file.FileName.Split("-")[0];
                var path = Path.Combine(_host.WebRootPath, "tjqxj", "uploadimg", fileId);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                using (var fs = System.IO.File.Create(Path.Combine(path, file.FileName.Split("-")[1])))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            else
            {
                var temp = JsonHelper.DeserializeStringToDictionary<string, string>(Request.Form["json"]);

                var db = new EFContext();
                var feedBack = new FeedBack();
                feedBack.ID = Convert.ToInt64(temp["id"]);

                var path = Path.Combine(_host.WebRootPath, "tjqxj", "uploadimg", temp["id"]);
                if (Directory.Exists(path))//此路径存在，证明有照片
                {
                    var files = Directory.GetFiles(path, "*.*").Select(u => u.Replace(_host.WebRootPath, ""));
                    feedBack.Images = string.Join('#', files);
                }

                feedBack.Address = temp["address"];
                feedBack.Climate = temp["climate"];
                feedBack.Remark = temp["remark"];
                feedBack.IsHandled = 0;
                feedBack.IsThank = 0;
                feedBack.OpenId = temp["openid"];
                feedBack.UploadTime = DateTime.Now;
                db.FeedBacks.Add(feedBack);
                db.SaveChanges();
                LogHelper.Info("收到灾情反馈：" + Request.Form["json"]);
            }
            return Content("success");
        }

        public IActionResult Success()
        {
            if (Request.Query.ContainsKey("headTitle")) ViewData["headTitle"] = Request.Query["headTitle"];
            if (Request.Query.ContainsKey("desc")) ViewData["desc"] = Request.Query["desc"];

            var requestUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            var signVm = WxCommonService.GetJsSignVm(requestUrl);
            return View(signVm);
        }

        //查看用户上传的灾情反馈信息
        public IActionResult GetFeedBack()
        {
            var requestUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            var signVm = WxCommonService.GetJsSignVm(requestUrl);
            var type = HttpContext.Request.Query.ContainsKey("type")
                ? Convert.ToInt32(HttpContext.Request.Query["type"])
                : 0;
            var db = new EFContext();
            var feedBacks = db.FeedBacks.Where(u => u.IsHandled == type).OrderByDescending(u => u.UploadTime).ToList();

            return View(new GetFeedBackVm() { SignVm = signVm, FeedBacks = feedBacks, Type = type, CurrentHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}" });
        }

        public IActionResult ThankFeedbackUser()
        {
            var temp = JsonHelper.DeserializeStringToDictionary<string, string>(Request.Form["json"]);
            var db = new EFContext();
            var feedBack = db.FeedBacks.Single(u => u.ID == Convert.ToInt64(temp["id"]));
            if (feedBack.IsThank == 1) return Content("success");
            var templateId = WxCommonService.GetTemplateId("感谢您的反馈");
            var feedBackTemplate = new FeedBackTemplate();
            feedBackTemplate.Address.Value = feedBack.Address;
            feedBackTemplate.Climate.Value = feedBack.Climate;
            feedBackTemplate.FeedbackTime.Value = feedBack.UploadTime.ToString("yyyy年MM月dd日hh时mm分ss秒");
            if (WxCommonService.SendMsgToUser(feedBack.OpenId, templateId, feedBackTemplate))
            {
                feedBack.IsThank = 1;
                db.SaveChanges();
                return Content("success");
            }
            return Content("false");
        }

        public IActionResult ChangeType()
        {
            var temp = JsonHelper.DeserializeStringToDictionary<string, string>(Request.Form["json"]);
            var db = new EFContext();
            var feedBack = db.FeedBacks.Single(u => u.ID == Convert.ToInt64(temp["id"]));
            feedBack.IsHandled = Convert.ToInt32(temp["type"]) == 0 ? 1 : 0;
            db.SaveChanges();
            return Content("success");
        }

        //用户留言
        public IActionResult UserAdvise()
        {
            var code = Request.Query.ContainsKey("CODE") ? Request.Query["CODE"].ToString() : "";
            ViewData["openId"] = WxCommonService.GetOpenId(code);
            return View();
        }

        //保存用户留言
        public IActionResult SaveUserAdvise()
        {
            var temp = JsonHelper.DeserializeStringToDictionary<string, string>(Request.Form["json"]);
            var db = new EFContext();
            var userAdvise = new Useradvise();
            userAdvise.UploadTime = DateTime.Now;
            userAdvise.Msg = temp["msg"];
            userAdvise.IsReply = 0;
            userAdvise.OpenId = temp["openId"];
            db.UserAdvises.Add(userAdvise);

            db.SaveChanges();
            return Content("success");
        }

        //获取用户留言记录
        public IActionResult GetUserAdvise()
        {
            var db = new EFContext();
            var userInfos = db.UserAdvises.OrderBy(u => u.UploadTime).ToList();
            return View(new GetMsgVm().FormDb(userInfos));
        }

        //删除用户留言
        public IActionResult DeleteUserAdvise()
        {
            var temp = JsonHelper.DeserializeStringToDictionary<string, string>(Request.Form["json"]);
            var db = new EFContext();
            var userAdvise = db.UserAdvises.Single(u => u.ID == Convert.ToInt64(temp["id"]));
            db.UserAdvises.Remove(userAdvise);
            db.SaveChanges();
            return Content("success");
        }

        //保存用户回复
        public IActionResult SaveReply()
        {
            var temp = JsonHelper.DeserializeStringToDictionary<string, string>(Request.Form["json"]);
            var db = new EFContext();
            var userAdvise = db.UserAdvises.Single(u => u.ID == Convert.ToInt64(temp["id"]));
            userAdvise.IsReply = 1;
            userAdvise.ReplyMsg = temp["replyMsg"];
            var templateId = WxCommonService.GetTemplateId("回复用户");
            var data = new ReplyTemplate();
            data.UserMsg.Value = userAdvise.Msg;
            data.ReplyMsg.Value = userAdvise.ReplyMsg;
            data.Time.Value = userAdvise.UploadTime.ToString("yyyy年MM月dd日hh时mm分ss秒");
            if (WxCommonService.SendMsgToUser(userAdvise.OpenId, templateId, data))
            {
                db.SaveChanges();
                return Content("success");
            }
            return Content("fail");
        }

        //上传产品页面
        public IActionResult UploadProduct()
        {
            var requestUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            LogHelper.Info("请求上传产品完整参数：" + requestUrl);
            var code = Request.Query.ContainsKey("CODE") ? Request.Query["CODE"].ToString() : "";
            var signVm = WxCommonService.GetJsSignVm(requestUrl);
            if (!string.IsNullOrEmpty(code))
            {
                signVm.OpenId = WxCommonService.GetOpenId(code);
            }
            return View(signVm);
        }

        //保存用户上传的产品信息，只会上传文件，每一个上传就是一个产品
        public IActionResult SaveProduct()
        {
            //保存图片
            var file = Request.Form.Files[0];
            var productType = file.FileName.Split("-")[0];
            var path = Path.Combine(_host.WebRootPath, "tjqxj", "uploadproduct");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var fileName = file.FileName.Split("-")[1];//包含了文件后缀
            var extension = Path.GetExtension(fileName);
            var fileWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            using (var fs = System.IO.File.Create(Path.Combine(path, EncryptHelper.GetMd5(fileWithoutExtension)+extension)))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            //保存其它信息
            var db = new EFContext();
            var productHistory = db.Products.SingleOrDefault(u => u.ProductTitle == fileName && u.ProductType==productType);
            if(productHistory != null) db.Products.Remove(productHistory);//删除历史信息
            var product = new Product
            {
                ProductType = productType,
                ProductTitle = fileWithoutExtension,
                UploadTime = DateTime.Now
            };
            product.ImgPath = $"/tjqxj/uploadproduct/{EncryptHelper.GetMd5(fileWithoutExtension) + extension}";
            db.Products.Add(product);
            if (db.SaveChanges()==1) return Content("success");
            return Content("fail");
        }

        //搜索产品
        public IActionResult SearchProduct()
        {
            //产品类型：4种
            var productType = "预警信号";
            if (Request.Query.ContainsKey("productType")) productType = Request.Query["productType"];

            var searchWord = "";
            if (Request.Query.ContainsKey("searchWord")) searchWord = Request.Query["searchWord"];

            var db = new EFContext();
            var dbList = db.Products.Where(u => u.ProductType == productType && u.ProductTitle.Contains(searchWord)).ToList();
            ViewData["productType"] = productType;
            var temp = new SearchProductVm();
            temp.ProductType = productType;
            temp.SearchWord = searchWord;
            temp.Products = dbList;
            var requestUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            temp.SignVm = WxCommonService.GetJsSignVm(requestUrl);
            temp.CurrentHost = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            return View(temp);
        }

        public IActionResult GetWeather()
        {

            var longitude = Request.Query["long"].ToString();
            var latitude = Request.Query["lat"].ToString();
            var realTimeInfo = CaiyunService.GetRealtimeInfo(longitude, latitude);

            var getWeatherVm = new GetWeatherVm();
            var realtimeClimateInfo = new RealtimeClimateInfo();
            getWeatherVm.RealtimeClimateInfo = realtimeClimateInfo;
            realtimeClimateInfo.Address = Request.Query["address"].ToString();
            realtimeClimateInfo.AQI = realTimeInfo.Item5;
            //otherRealtimeInfo.NearRainDistance = realTimeInfo.Item4;
            realtimeClimateInfo.PM = realTimeInfo.Item3;
            realtimeClimateInfo.Tempture = realTimeInfo.Item6;

            getWeatherVm.ClimateInfo = CaiyunService.GetPrediction(realtimeClimateInfo, longitude, latitude).Item2;

            return View(getWeatherVm);
        }

        public IActionResult TryWeather()
        {
            var requestUrl =
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";
            var signVm = WxCommonService.GetJsSignVm(requestUrl);
            return View(signVm);
        }
    }
}
