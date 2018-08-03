using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MySoft.Weeding.Filter
{
    public class GloabException:IExceptionFilter
    {
        public async void OnException(ExceptionContext context)
        {
#if DEBUG
            context.Result = new ClientResult(ResultDto.DefaultError((int)ResultState.Unknow, context.Exception.Message + Environment.NewLine + context.Exception.StackTrace));
#else
            context.Result = new ClientResult(ResultDto.DefaultError((int)ResultState.Unknow, "未知错误"));
#endif
            MySoft.Common.LogHelper.Error(context.Exception.Message + Environment.NewLine + context.Exception.StackTrace);
            context.ExceptionHandled = true;
        }
    }
}
