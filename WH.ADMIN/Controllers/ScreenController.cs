using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Models.Entities;
using WH.ADMIN.Models.RequestResponse;
using WH.ADMIN.Services;

namespace WH.ADMIN.Controllers
{
    [Authorize]
    [ValidateModel]
    [HandleException]
    [Route("api/screen")]
    [ApiController]
    public class ScreenController : ControllerBase
    {
        [HttpGet("get-screen-details/{screenId}")]
        public IActionResult GetScreenDetails(long screenId) { 
            ScreenService service = new ScreenService();
            var result = service.GetScreenDetails(screenId);

            if (result == null) {
                return HttpHelper.Failed("Screen doesn't exist.");
            }

            var response = new GetScreenDetailsResponse(result);
            return HttpHelper.Success(response);
        }

        [HttpGet("get-screen-list/{branchId}")]
        public IActionResult GetScreenList(long branchId) {
            ScreenService service = new ScreenService();
            var result = service.GetScreenList(branchId);
            var response = result?.Select(x => new GetScreenListResponse(x)).ToList();
            return HttpHelper.Success(response);
        }


        [HttpPost("add-screen")]
        public IActionResult AddScreen(AddScreenRequest request)
        {

            Session session = new Session(HttpContext.User);
            ScreenService service = new ScreenService();
            var result = service.AddScreen(new Screens(request), session);

            if (!result.IsSuccess) {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }

        [HttpPut("update-screen")]
        public IActionResult UpdateScreen(UpdateScreenRequest request) {
            Session session = new Session(HttpContext.User);
            ScreenService service = new ScreenService();
            var result = service.UpdateScreen(new Screens(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }


        [HttpDelete("delete-screen/{screenId}")]
        public IActionResult DeleteScreen(long screenId)
        {
            Session session = new Session(HttpContext.User);
            ScreenService service = new ScreenService();
            var result = service.DeleteScreen(screenId, session);
            
            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }



    }
}
