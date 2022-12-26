using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using Utilities;
using WH.ADMIN.Helper;
using WH.ADMIN.Models.RequestResponse;
using WH.ADMIN.Services;

namespace WH.ADMIN.Controllers
{
    [ValidateModel]
    [HandleException]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesDefaultResponseType(typeof(LoginResponse))]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            AuthService service = new AuthService();
            var result = service.Login(request.Username,request.Password);

            if (!result.IsSuccess) {
                return HttpHelper.Failed(result.Message);
            }

            var user = result.Data;
            var token = TokenHelper.GenerateToken(new Models.Token.UserDetails(user));

            LoginResponse response = new LoginResponse(user);
            response.Token = token;

            return HttpHelper.Success(response, result.Message);
        }

    }
}
