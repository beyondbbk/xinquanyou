using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mysoft.Tjqxj.Models;
using Mysoft.Tjqxj.Models.Template;
using Mysoft.Tjqxj.Models.ViewModel;
using MySoft.Common;

namespace Mysoft.Tjqxj.Services
{
    public class WxCommonService
    {
        private static readonly string WxAppId = JsonConfigHelper.Get<string>("WxAppId");
        private static readonly string AppSecret = JsonConfigHelper.Get<string>("AppSecret");
        private static readonly string WxApiUrl = "https://api.weixin.qq.com/cgi-bin/token";
        private static string _accessToken = "";
        private static DateTime _tokenExpireTime = DateTime.Now;

        private static string _jsTicket = "";
        private static DateTime _jsTicketExpireTime = DateTime.Now;


        /// <summary>
        /// 获得Token
        /// </summary>
        /// <returns></returns>
        private static string GetAccessToken()
        {
            //token不为空，并且还没过期，直接返回
            if (!string.IsNullOrEmpty(_accessToken) && (_tokenExpireTime > DateTime.Now))
                return _accessToken;

            //重新获取Token
            var getStr = $"grant_type=client_credential&appid={WxAppId}&secret={AppSecret}";
            var result = HttpHelper.Get(WxApiUrl, getStr);
            var temp = JsonHelper.DeserializeStringToDictionary<string, object>(result);
            _accessToken = temp["access_token"].ToString();
            _tokenExpireTime = DateTime.Now.AddSeconds(Convert.ToInt32(temp["expires_in"])-500);//Token提前500秒失效
            return _accessToken;
        }

        /// <summary>
        /// 获得jsTicket
        /// </summary>
        /// <returns></returns>
        private static string GetJsTicket()
        {
            //jsticket不为空，并且还没过期，直接返回
            if (!string.IsNullOrEmpty(_jsTicket) && (_jsTicketExpireTime > DateTime.Now))
                return _jsTicket;

            //重新获取jsticket
            string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket";
            string getData = "access_token=" + GetAccessToken() + "&type=jsapi";
            var result = HttpHelper.Get(url, getData);
            var dic = JsonHelper.DeserializeStringToDictionary<string, object>(result);
            _jsTicket = dic["ticket"].ToString();
            _jsTicketExpireTime = DateTime.Now.AddSeconds(Convert.ToInt32(dic["expires_in"]) - 500);//Token提前500秒失效
            return _jsTicket;
        }

        /// <summary>
        /// 获取js签名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static SignVm GetJsSignVm(string url)
        {
            var sortedDic = new SortedDictionary<string, string>
            {
                { "noncestr", RandomHelper.Create() },
                { "jsapi_ticket", GetJsTicket() },
                { "timestamp", CommonHelper.GetTimestamp(DateTime.Now).ToString() },
                { "url", url }
            };
            var jsSign = EncryptHelper.GetSha1(CommonHelper.CreateGetStr(sortedDic)).ToLower();
            var temp = new SignVm();
            temp.AppId = WxAppId;
            temp.NonceStr = sortedDic["noncestr"];
            temp.Signature = jsSign;
            temp.TimeStamp = sortedDic["timestamp"];
            return temp;
        }

        public static string GetOpenId(string code)
        {
            //重新获取jsticket
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token";
            string getData = $"appid={WxAppId}&secret={AppSecret}&code={code}&grant_type=authorization_code";
            var result = HttpHelper.Get(url, getData);
            var dic = JsonHelper.DeserializeStringToDictionary<string, object>(result);
            if (!dic.ContainsKey("openid")) return "";//code可能过期了
            return dic["openid"].ToString();
        }

        /// <summary>
        /// 根据约定的模板标题获取模板Id
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string GetTemplateId(string title)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/template/get_all_private_template";
            string getData = $"access_token={GetAccessToken()}";
            var result = HttpHelper.Get(url, getData);
            var dic = JsonHelper.DeserializeStringToDictionary<string, List<object>>(result);
            foreach (var template in dic["template_list"])
            {
                var temp = JsonHelper.DeserializeStringToDictionary<string, string>(template.ToString());
                if (temp["title"] == title) return temp["template_id"];
            }
            LogHelper.Error($"模板未能找到，期望标题为{title}{Environment.NewLine}实际结果：{result}");
            return null;
        }

        public static bool SendMsgToUser(string openId,string templateId,object data)
        {
            //重新获取jsticket
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + GetAccessToken();
            var postDic = new Dictionary<string,object>();
            postDic.Add("touser",openId);
            postDic.Add("template_id", templateId);
            postDic.Add("appid", WxAppId);
            postDic.Add("data", data);
            var result = HttpHelper.Post(url, JsonHelper.Serialize(postDic));
            var res = JsonHelper.Serialize(postDic);
            var dic = JsonHelper.DeserializeStringToDictionary<string, string>(result);
            LogHelper.Info("消息发送结果："+JsonHelper.Serialize(dic));
            return dic["errcode"]=="0";
        }
    }
}
