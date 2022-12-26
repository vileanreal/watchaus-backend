using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;
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

            List<GetUserListResponse> response = result.Select(x => new GetUserListResponse
            {
                UserId = x.UserId,
                Username = x.Username,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNo = x.PhoneNo,
                BranchId = x.BranchId,
                BranchName = x.BranchName,
                RoleId = x.RoleId,
                RoleName = x.RoleName
            }).ToList();

            return HttpHelper.Success(response);
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] AddUserRequest request)
        {
            Session session = new Session(HttpContext.User);

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
