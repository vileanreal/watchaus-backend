using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Utilities
{
    public class HandleException : Attribute, IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var ex = context.Exception;
            Log.Error("An internal error occured. {Exception}", ex);
            context.Result = HttpHelper.Failed(500, $"An internal error occured. {ex.Message}");
        }
    }
}
