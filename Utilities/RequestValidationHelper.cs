using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public int Code { get; set; }
        public string Message { get; }

        public ValidationError(string field, int code, string message)
        {
            Field = field != string.Empty ? field : null;
            //Code = code != 0 ? code : 55;  //set the default code to 55. you can remove it or change it to 400.
            Message = message;
        }
    }
    public class ValidationResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; }
        public object? Data { get; set; }
        public List<ValidationError> Errors { get; }
        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Data = null;
            IsSuccess = false;
            Message = "Validation Failed";
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, 0, x.ErrorMessage)))
                    .ToList();

        }
    }

    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState) : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }


    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }


    // ADD TO PROGRAM
    //builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    //        {
    //    options.InvalidModelStateResponseFactory = context =>
    //    {
    //        var result = new ValidationFailedResult(context.ModelState);
    //        result.ContentTypes.Add(MediaTypeNames.Application.Json);
    //        return result;
    //    };
    //}); ;





}
