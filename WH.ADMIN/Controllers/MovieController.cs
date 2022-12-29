﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
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
    [Route("api/movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        [HttpGet("get-movie-details/{movieId}")]
        [ProducesDefaultResponseType(typeof(GetMovieDetailsResponse))]
        public IActionResult GetMovieDetails(long movieId) {
            MovieService service = new MovieService();
            var movie = service.GetMovieDetails(movieId);
            var response = new GetMovieDetailsResponse(movie);
            return HttpHelper.Success(response);
        }

        [HttpPost("get-movie-list")]
        [ProducesDefaultResponseType(typeof(List<GetMovieListResponse>))]
        public IActionResult GetMovieList()
        {
            MovieService service = new MovieService();
            var result = service.GetMovieList();
            var response = result.Select(x => new GetMovieListResponse(x)).ToList();
            return HttpHelper.Success(response);
        }


        [HttpPost("add-movie")]
        public IActionResult AddMovie([FromBody] AddMovieRequest request) {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);
            }

            MovieService service = new MovieService();
            var result = service.AddMovie(new Movies(request), session);

            if (!result.IsSuccess) {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }


        [HttpPut("update-movie")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieRequest request) {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);
            }

            MovieService service = new MovieService();
            var result = service.UpdateMovie(new Movies(request), session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }

            return HttpHelper.Success(result.Message);
        }

        [HttpDelete("delete-movie/{movieId}")]
        public IActionResult DeleteMovie(long movieId) {
            Session session = new Session(HttpContext.User);

            if (session.RoleId != Roles.SUPERADMIN &&
                session.RoleId != Roles.ADMIN)
            {
                return HttpHelper.Failed(401, ResponseMessages.ONLY_AN_ADMIN_CAN_PERFORM_THIS_ACTION);

            }

            MovieService service = new MovieService();
            var result = service.DeleteMovie(movieId, session);

            if (!result.IsSuccess)
            {
                return HttpHelper.Failed(result.Message);
            }
            return HttpHelper.Success();
        }

    }
}