using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySoft.Common;
using MySoft.Weeding.Models;
using MySoft.Weeding.Models.Request;
using MySoft.Weeding.Models.Respone;
using MySoft.Weeding.Services;

namespace MySoft.Weeding.Controllers
{
    public class HomeController : Controller
    {
        private static object ob = new object();
        //访问记录码
        private static List<string> TimeIds=new List<string>();

        private static List<FriendInfo> friendInfos = new List<FriendInfo>()
        {
            //公司研发部
            new FriendInfo(){urlName = "匿名",NameInfos = new List<string>(){"你"}},
            new FriendInfo(){urlName = "dataoge",NameInfos = new List<string>(){"李涛","大涛哥","大涛姥爷"},PhoneNo = "13096306603"},
            new FriendInfo(){urlName = "taoge",NameInfos = new List<string>(){"杨涛","涛哥","涛姥爷"},PhoneNo = "18980594170"},
            new FriendInfo(){urlName = "dapengge",NameInfos = new List<string>(){"彭积祥","大彭哥"},PhoneNo = "13980952841"},
            new FriendInfo(){urlName = "xiaogao",NameInfos = new List<string>(){"高田","小高"},PhoneNo = "13548123102"},
            new FriendInfo(){urlName = "xiaoluo",NameInfos = new List<string>(){"木色小罗","小罗"},PhoneNo = "18980811704"},
            new FriendInfo(){urlName = "laowei",NameInfos = new List<string>(){"魏先伟","老魏"},PhoneNo = "13980667369"},
            new FriendInfo(){urlName = "huihui",NameInfos = new List<string>(){"巩辉","辉辉"},PhoneNo = "18782966015"},
            new FriendInfo(){urlName = "fulin",NameInfos = new List<string>(){ "蒲涪陵", "蒲兄"},PhoneNo = "17308029074"},
            new FriendInfo(){urlName = "xudaye",NameInfos = new List<string>(){ "吴旭", "旭大爷"},PhoneNo = "13540490160"},

            //测试部
            new FriendInfo(){urlName = "xiaobei",NameInfos = new List<string>(){"杜雨蓓","小蓓"},PhoneNo = "18328402368"},
            new FriendInfo(){urlName = "hehe",NameInfos = new List<string>(){"何鹏英","何何"},PhoneNo = "13458555638"},
            new FriendInfo(){urlName = "xiaowen",NameInfos = new List<string>(){"文郭","小文"},PhoneNo = "18215581090"},
            new FriendInfo(){urlName = "xiaofeng",NameInfos = new List<string>(){ "何晓凤", "晓凤"},PhoneNo = "13668125804"},

            //运营
            new FriendInfo(){urlName = "sunlaoban",NameInfos = new List<string>(){ "孙劭恒", "太阳先生","孙老板"},PhoneNo = "18108180803"},
            new FriendInfo(){urlName = "xiaohong",NameInfos = new List<string>(){ "王洪"},PhoneNo = "18408219980"},
            new FriendInfo(){urlName = "xiaoxia",NameInfos = new List<string>(){ "夏静维", "小夏","小夏妹子"},PhoneNo = "18280427903"},
            new FriendInfo(){urlName = "wangmanman",NameInfos = new List<string>(){ "王蕾", "王慢慢"},PhoneNo = "15208398294"},

            //产品
            new FriendInfo(){urlName = "xiaoli",NameInfos = new List<string>(){ "罗英丽", "小丽"},PhoneNo = "15982210927"},
            new FriendInfo(){urlName = "xiaoxue",NameInfos = new List<string>(){ "邹雪", "小雪"},PhoneNo = "13219930295"},

            //104寝室几爷子
            new FriendInfo(){urlName = "jiwei",NameInfos = new List<string>(){ "张吉蔚", "老大"},PhoneNo = "18222011785"},
            new FriendInfo(){urlName = "yunzhuang",NameInfos = new List<string>(){ "张泳", "泳娃","云装"},PhoneNo = "13281130315"},
            new FriendInfo(){urlName = "zhapi",NameInfos = new List<string>(){ "王钟平", "扎皮"},PhoneNo = "17313020001"},
            new FriendInfo(){urlName = "haose",NameInfos = new List<string>(){ "杨浩", "浩涩"},PhoneNo = "13032849343"},
            new FriendInfo(){urlName = "tonger",NameInfos = new List<string>(){ "祝建华", "童儿"},PhoneNo = "15884477757"},
            new FriendInfo(){urlName = "panxuan",NameInfos = new List<string>(){ "潘暄"}},
            new FriendInfo(){urlName = "zlb",NameInfos = new List<string>(){ "赵利彬", "ZLB"}},

            //中软几爷子
            new FriendInfo(){urlName = "taiping",NameInfos = new List<string>(){ "何泰平", "泰平儿"},PhoneNo = "15928548365"},
        };
        

        public IActionResult Index(PersonInfoReq person)
        {
            
            if (string.IsNullOrEmpty(person.Name))
            {
                person.Name = "匿名";
            }
            if (string.IsNullOrEmpty(person.TimeId))
            {
                person.TimeId = "";
            }
            var homeRes = new HomeRes();

            if (!TimeIds.Contains(person.TimeId))
            {
                homeRes.IsReload = true;
                TimeIds.Add(homeRes.TimeId);
            }
            else
            {
                homeRes.IsReload = false;
                TimeIds.Remove(person.TimeId);
            }

                var firstDate = Convert.ToDateTime("2018-05-19 00:00:00");
            var togetherDate = DateTime.Now - firstDate;
            var weedDate = Convert.ToDateTime("2018-09-24 00:00:00");
            var prepareDate = weedDate - DateTime.Now;
            homeRes.TogetherDays = togetherDate.Days;
            homeRes.PrepareDays = prepareDate.Days;
            homeRes.FriendInfo = GetFriendInfo(person.Name);
            return View(homeRes);
        }

        public IActionResult WebLog(WebLogReq webLogReq)
        {
            LogHelper.Info($"Web日志反馈：时间-{webLogReq.Time} 来源-{webLogReq.Name} 内容-{webLogReq.LogMsg}");
            return Json(ResultDto.DefaultSuccess("反馈成功"));
        }

        private FriendInfo GetFriendInfo(string name)
        {
            var temp = friendInfos.SingleOrDefault(u => u.urlName == name);
            if (temp != null)
            {
                return temp;
            }
            return friendInfos.SingleOrDefault(u => u.urlName == "匿名");
        }


        public IActionResult SendSms(PersonInfoReq person)
        {
            if (string.IsNullOrEmpty(person.Name)) return Content("url-name参数错误");
            var temp = friendInfos.SingleOrDefault(u => u.urlName == person.Name);
            if(temp==null) return Content("无此人的信息");
            if (string.IsNullOrEmpty(temp.PhoneNo)) return Content("此人电话号码为空");
            var result = SmsHelper.SendSms( temp.PhoneNo, temp.NameInfos[0]);
            LogHelper.Info($"短信发送结果：{result} 发给{temp.NameInfos[0]} 目标号码：{temp.PhoneNo}");
            if (result)
            {
                return Content("success");
            }
            return Content("短信发送失败");
        }

        public IActionResult GetWordArr(GetStrReq getStrVm)
        {
 
            lock (ob)
            {
               var result = CreateStrArray.GetFormText(getStrVm.Text, getStrVm.FontSize, getStrVm.FontName);
                return new ClientResult(ResultDto.DefaultSuccess(result));
            }
        }


        public IActionResult GetImgArr(GetImgReq getImgVm)
        {
            lock (ob)
            {
                var result = CreateStrArray.GetFormBmp(getImgVm.ImgName,getImgVm.Width,getImgVm.Height);
                return new ClientResult(ResultDto.DefaultSuccess(result));
            }
        }

    }
}
