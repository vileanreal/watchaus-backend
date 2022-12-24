using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities;
using WH.ADMIN.Helper;
using WH.ADMIN.Models.Entities;
using WH.ADMIN.Models.RequestResponse;
using WH.ADMIN.Services;

namespace WH.ADMIN.Controllers
{
    [Authorize]
    [ValidateModel]
    [HandleException]
    [Route("api/admin/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] AddUserRequest request)
        {
            Session session = new Session(HttpContext.User);

            UserService service = new UserService();
            var newUser = new User(request);
            var result = service.AddUser(newUser, session.Id);
            
            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(message: result.Message);
        }
    }
}
