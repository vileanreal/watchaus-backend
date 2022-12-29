using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
    [Route("api/branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {

        [HttpPost("get-branch-list")]
        [ProducesDefaultResponseType(typeof(List<GetBranchListResponse>))]
        public IActionResult GetBranchList()
        {
            var service = new BranchService();
            var list = service.GetBranchList();

            var response = list.Select(item => new GetBranchListResponse(item)).ToList();

            return HttpHelper.Success(response);
        }

        
        [HttpPost("add-branch")]
        public IActionResult AddBranch([FromBody] AddBranchRequest request)
        {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);

            }

            var service = new BranchService();
            var result = service.AddBranch(new Branch(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }



        [HttpPut("update-branch")]
        public IActionResult UpdateBranch([FromBody] UpdateBranchRequest request)
        {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);

            }

            var service = new BranchService();
            var result = service.UpdateBranch(new Branch(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }


        [HttpDelete("delete-branch/{branchId}")]
        public IActionResult DeleteBranch(long branchId)
        {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);

            }

            var service = new BranchService();
            var result = service.DeleteBranch(branchId, session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }

        


    }
}
