using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Utilities;
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

            var response = list.Select(item => new GetBranchListResponse()
            {
                BranchId = item.BranchId,
                Name = item.Name
            }).ToList();

            return HttpHelper.Success(response);
        }

        
        [HttpPost("add-branch")]
        public IActionResult AddBranch([FromBody] AddBranchRequest request)
        {
            Session session = new Session(HttpContext.User);

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

            var service = new BranchService();
            var result = service.UpdateBranch(new Branch(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }


        [HttpDelete("delete-branch")]
        public IActionResult DeleteBranch([FromBody] DeleteBranchRequest request)
        {
            Session session = new Session(HttpContext.User);

            var service = new BranchService();
            var result = service.DeleteBranch(new Branch(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }

        


    }
}
