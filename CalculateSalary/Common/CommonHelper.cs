using System;
using System.Collections.Generic;
using System.Text;

namespace MySoft.Common
{
    public class CommonHelper
    {
        /// <summary>    
        /// DateTime时间格式转换为Unix时间戳格式    
        /// </summary>    
        /// <param name="time"> DateTime时间格式</param>    
        /// <returns>Unix时间戳格式</returns>    
        public static int GetTimestamp(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public static string CreateGetStr(IDictionary<string,string> dic)
        {
            var result = "";
            foreach (var temp in dic)
            {
                result += temp.Key + "=" + temp.Value + "&";
            }
            return result.TrimEnd('&');
        }
    }
}
