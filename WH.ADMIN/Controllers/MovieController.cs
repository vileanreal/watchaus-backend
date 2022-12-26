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
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        [HttpPost("add-movie")]
        public IActionResult AddMovie([FromBody] AddMovieRequest request) {
            Session session = new Session(HttpContext.User);
            MovieService service = new MovieService();
            var result = service.AddMovie(new Movies(request), session);

            if (!result.IsSuccess) {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }
    }
}
