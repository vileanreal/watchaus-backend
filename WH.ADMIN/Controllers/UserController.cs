using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;
using WH.ADMIN.Models.RequestResponse;
using WH.ADMIN.Services;

namespace WH.ADMIN.Controllers
{
    [Authorize]
    [ValidateModel]
    [HandleException]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("get-role-list")]
        [ProducesDefaultResponseType(typeof(List<GetRoleListResponse>))]
        public IActionResult GetRoleList()
        {
            var roles = new List<GetRoleListResponse>() {
                new GetRoleListResponse() {
                    RoleId = 1,
                    RoleName = "ADMIN"
                },
                new GetRoleListResponse() {
                    RoleId = 2,
                    RoleName = "OPERATOR"
                }
            };
            return HttpHelper.Success(roles);
        }

        [HttpPost("get-user-list")]
        [ProducesDefaultResponseType(typeof(List<GetUserListResponse>))]
        public IActionResult GetUserList()
        {

            var service = new UserService();
            var result = service.GetUserList();

            List<GetUserListResponse> response = result.Select(x => new GetUserListResponse(x)).ToList();

            return HttpHelper.Success(response);
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] AddUserRequest request)
        {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);

            }

            UserService service = new UserService();
            var newUser = new User(request);
            var result = service.AddUser(newUser, session);
            
            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(message: result.Message);
        }


        [HttpPut("update-user")]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request) {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);

            }

            UserService service = new UserService();
            var newUser = new User(request);
            var result = service.UpdateUser(newUser, session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(message: result.Message);
        }

        


        [HttpDelete("delete-user/{username}")]
        public IActionResult DeleteUser(string username)
        {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);

            }

            var service = new UserService();
            var result = service.DeleteUser(username, session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(message: result.Message);
        }

        
    }
}
