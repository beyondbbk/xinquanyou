using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySoft.Common;
using Newtonsoft.Json;

namespace MySoft.Weeding
{
    /// <summary>
    /// 接口返回对象类
    /// </summary>
    public class ClientResult : JsonResult
    {
        private static readonly JsonSerializerSettings Setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
        public ClientResult(ResultDto result) : base(result, Setting)
        {

        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context为空");
            }

            var response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "text/plain";
            }

            if (Value == null)
            {
                Value = new ResultDto { State = (int)ResultState.Unknow, Result = "未知错误" };
            }

            var ret = JsonConvert.SerializeObject(Value, Setting);

            await response.WriteAsync(ret);

            var milliSeconds = -1;
            if (Int32.TryParse(context.HttpContext.Request.Headers["X-Time"], out Int32 startMilliSecond))
            {
                milliSeconds = Environment.TickCount - startMilliSecond;
            }
            var request = context.HttpContext.Request;

            LogHelper.Info($"耗时：{milliSeconds}毫秒\n\r" +
                                $"请求地址：{request.Host + request.Path}{(!request.QueryString.HasValue ? "" : request.QueryString.ToString())}\n\r" +
                                $"Post参数：{(Convert.ToInt32(request.ContentLength) == 0 ? "无" : GetRequestBodyStr(context))}\n\r" +
                                $"响应参数：{ret}");
        }
        private string GetRequestBodyStr(ActionContext filterContext)
        {
            var result = "";
            var request = filterContext.HttpContext.Request;
            foreach (var key in request.Form.Keys)
            {
                result += $"{key}:{request.Form[key]} ";
            }
            return result.Replace("\n\r","换行符");
        }
    }
}