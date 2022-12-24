using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using WH.PORTAL.Models.Entities;
using WH.PORTAL.Models.RequestResponse;
using WH.PORTAL.Services;

namespace WH.PORTAL.Controllers
{
    [ValidateModel]
    [HandleException]
    [Route("api/web/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            CustomerService service = new CustomerService();
            var result = service.Register(new Customers(request));
            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }
            return HttpHelper.Success(null, result.Message);
        }


        [HttpPost("confirm-email")]
        public IActionResult ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            CustomerService service = new CustomerService();
            var result = service.ConfirmEmail(request.CustomerId, request.LinkCode);
            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }
            return HttpHelper.Success(result.Message);
        }
    }
}
