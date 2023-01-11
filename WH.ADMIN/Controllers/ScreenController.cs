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
    [Route("api/screen")]
    [ApiController]
    public class ScreenController : ControllerBase
    {
        [HttpGet("get-screen-details/{screenId}")]
        public IActionResult GetScreenDetails(long screenId)
        {
            ScreenService service = new ScreenService();
            var result = service.GetScreenDetails(screenId);

            if (result == null)
            {
                return HttpHelper.Failed("Screen doesn't exist.");
            }

            var response = new GetScreenDetailsResponse(result);
            return HttpHelper.Success(response);
        }

        [HttpGet("get-screen-list/{branchId}")]
        public IActionResult GetScreenList(long branchId)
        {
            ScreenService service = new ScreenService();
            var result = service.GetScreenList(branchId);
            var response = result?.Select(x => new GetScreenListResponse(x)).ToList();
            return HttpHelper.Success(response);
        }

        [HttpGet("get-assigned-movie-list/{screenId}")]
        public IActionResult GetAssignedMovieList(long screenId)
        {
            ScreenService screenService = new ScreenService();
            MovieService movieService = new MovieService();

            var result = screenService.GetAssignedMovieList(screenId);
            var response = new List<GetAssignedMovieListResponse>();

            foreach (var item in result)
            {
                var movieDetails = movieService.GetMovieDetails(item.MovieId);

                response.Add(new GetAssignedMovieListResponse()
                {
                    SamId = item.SamId,
                    ScreenId = item.ScreenId,
                    MovieId = item.MovieId,
                    Title = movieDetails.Title,
                    MoviePosterImg = movieDetails.ImageList?.Find(x => x.IsMoviePoster)?.Base64Img ?? ""
                });
            }

            return HttpHelper.Success(response);
        }

        [HttpPost("add-screen")]
        public IActionResult AddScreen(AddScreenRequest request)
        {

            Session session = new Session(HttpContext.User);
            ScreenService service = new ScreenService();
            var result = service.AddScreen(new Screens(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }

        [HttpPut("update-screen")]
        public IActionResult UpdateScreen(UpdateScreenRequest request)
        {
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


        [HttpPost("assign-movie-to-screen")]
        public IActionResult AssignMovieToScreen([FromBody] AssignMovieToScreenRequest request)
        {
            Session session = new Session(HttpContext.User);

            ScreenService service = new ScreenService();
            var result = service.AssignMovieToScreen(new ScreenAssignedMovies(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }

        [HttpDelete("delete-assigned-movie")]
        public IActionResult DeleteAssignedMovie([FromBody] DeleteAssignedMovieRequest request)
        {
            Session session = new Session(HttpContext.User);

            ScreenService service = new ScreenService();
            var result = service.DeleteAssignedMovie(new ScreenAssignedMovies(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }

    }
}
