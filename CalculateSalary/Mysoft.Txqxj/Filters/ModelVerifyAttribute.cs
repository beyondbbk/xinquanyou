using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mysoft.Txqxj.Model.Enum;
using Mysoft.Txqxj.API.Models.Respond;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mysoft.Txqxj.API.Filters
{
    public class ModelVerifyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //加入时间戳-接口计时需要
            context.HttpContext.Request.Headers.Add("X-Time", Environment.TickCount.ToString());
            var err = "";
            foreach (var propname in context.ModelState.Keys)
            {
                foreach (var temp in context.ModelState[propname].Errors)
                {
                    err = $"参数{propname}错误，{temp.ErrorMessage}";
                }
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new ClientResult(ResultDto.DefaultError(ResultState.GlobalParameterError, err ));
            }
        }
    }
}
