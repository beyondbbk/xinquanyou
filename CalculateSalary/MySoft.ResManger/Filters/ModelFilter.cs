
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MySoft.ResManger.Filters
{
    public class ModelFilter: Attribute,IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string err="";
            foreach (var propname in context.ModelState.Keys)
            {
                foreach (var temp in context.ModelState[propname].Errors)
                {
                    err = propname + temp.ErrorMessage;
                }
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new{errMsg=err});
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }
    }
}
