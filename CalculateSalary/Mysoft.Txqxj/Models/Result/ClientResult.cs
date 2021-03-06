﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mysoft.Txqxj.API.Models.Respond;
using Mysoft.Txqxj.Model.Enum;
using MySoft.Common;
using Newtonsoft.Json;

namespace Mysoft.Txqxj
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

            response.StatusCode = 200;

            var request = context.HttpContext.Request;
            LogHelper.Info($"耗时：{milliSeconds}毫秒{Environment.NewLine}" +
                                $"请求地址：{request.Host + request.Path}{(!request.QueryString.HasValue ? "" : request.QueryString.ToString())}{Environment.NewLine}" +
                                $"Post参数：{(Convert.ToInt32(request.ContentLength) == 0 ? "无" : GetRequestBodyStr(context))}{Environment.NewLine}" +
                                $"响应参数：{ret}{Environment.NewLine}");
        }
        private string GetRequestBodyStr(ActionContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            request.Body.Position = 0;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            request.Body.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }
    }
}