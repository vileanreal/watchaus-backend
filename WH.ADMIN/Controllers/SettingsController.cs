using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Utilities;
using WH.ADMIN.Models.RequestResponse;
using WH.ADMIN.Services;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Controllers
{
    [Authorize]
    [ValidateModel]
    [HandleException]
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : Controller
    {
        [HttpPost("get-setting-list")]
        [ProducesDefaultResponseType(typeof(List<GetSettingsListResponse>))]
        public IActionResult GetSettingsList()
        {
            var service = new SettingsService();
            var list = service.GetSettingsList();

            var response = list.Select(item => new GetSettingsListResponse(item)).ToList();

            return HttpHelper.Success(response);
        }

        [HttpPut("update-settings")]
        public IActionResult UpdateSettings([FromBody] UpdateSettingsRequest request)
        {
            Session session = new Session(HttpContext.User);
            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);
            }
            
            SettingsService service = new SettingsService();
            List<I_Settings> settingList = request.SettingList.Select(x => new I_Settings(x)).ToList();
            var result = service.UpdateSettings(settingList, session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }
            return HttpHelper.Success(result.Message);
        }
    }
}
