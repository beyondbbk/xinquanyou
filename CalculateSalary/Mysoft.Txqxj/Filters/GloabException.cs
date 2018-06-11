using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MySoft.Common;
using Mysoft.Txqxj.Model.Enum;
using Mysoft.Txqxj.API.Models.Respond;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mysoft.Txqxj.API.Filters
{
    public class GloabException:IExceptionFilter
    {
        public  void OnException(ExceptionContext context)
        {
#if DEBUG
            context.Result = new ClientResult(ResultDto.DefaultError((int)ResultState.Unknow, context.Exception.Message + Environment.NewLine + context.Exception.StackTrace));
#else
            context.Result = new ClientResult(ResultDto.DefaultError((int)ResultState.Unknow, "未知错误"));
#endif
            LogHelper.Error(context.Exception.Message + Environment.NewLine + context.Exception.StackTrace);
            context.ExceptionHandled = true;
        }
    }
}
