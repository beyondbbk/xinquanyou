using System;
using Microsoft.AspNetCore.Mvc.Filters;
using MySoft.Common;

namespace MySoft.Weeding.Filter
{
    public class GloabException:IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            LogHelper.Error("未捕获异常："+ context.Exception.Message + "\n\r" + context.Exception.StackTrace);
#if DEBUG
            context.Result = new ClientResult(ResultDto.DefaultError((int)ResultState.Unknow, context.Exception.Message + "\n\r" + context.Exception.StackTrace));
#else
            context.Result = new ClientResult(ResultDto.DefaultError((int)ResultState.Unknow, "未知错误"));
#endif
            MySoft.Common.LogHelper.Error(context.Exception.Message + Environment.NewLine + context.Exception.StackTrace);
            context.ExceptionHandled = true;
        }
    }
}
