using System;
using MySoft.Common;

namespace Mysoft.Tjqxj.Services
{
    public class HomeService
    {
        /// <summary>
        /// 在微信后台设置的验证token
        /// </summary>
        private static readonly string Token = JsonConfigHelper.Get<string>("WxToken");

        public static bool VerifyWx(string timestamp,string nonce,string wxsign)
        {
            string[] arrTmp = { Token, timestamp, nonce };
            Array.Sort(arrTmp); //字典排序
            string tmpStr = string.Join("", arrTmp);

            return EncryptHelper.GetSha1(tmpStr).ToLower() == wxsign;
        }
    }
}
