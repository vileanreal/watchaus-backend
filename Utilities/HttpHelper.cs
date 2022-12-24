using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class HttpHelper : ControllerBase
    {
        public static IActionResult Success(object? data, string? message = "Success")
        {
            var response = OperationResult<object>.Success(data, message);
            return new HttpHelper().Ok(response);
        }

        public static IActionResult Success(string? message = "Success")
        {
            var response = OperationResult<object>.Success(null, message);
            return new HttpHelper().Ok(response);
        }

        public static IActionResult Failed(string? message)
        {
            var response = OperationResult<object>.Failed(message);
            return new HttpHelper().BadRequest(response);
        }

        public static IActionResult FailedWithResult(object? data, string? message)
        {
            var response = OperationResult<object>.FailedWithResult(data, message);
            return new HttpHelper().BadRequest(response);
        }

        public static IActionResult Failed(int statusCode, string? message)
        {
            var response = OperationResult<object>.Failed(message);
            return new HttpHelper().StatusCode(statusCode, response);
        }
    }
}
