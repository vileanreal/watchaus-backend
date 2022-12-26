using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Models.Entities;
using WH.PORTAL.Services;

namespace WH.ADMIN.Services
{
    public class MovieService
    {

        public Movies GetMovieById(long movieId) { 
            using var manager = new MovieManager();
            return manager.GetMovie("movie_id", movieId.ToString());
        }

        public bool IsMovieExist(long movieId) { 
            var movie = GetMovieById(movieId);
            return movie != null;
        }

        public OperationResult AddMovie(Movies movie, Session session) {
            var userService = new UserService();
            var loggonedUser = userService.GetUserDetails(session.Username);
            var commonService = new CommonService();
            var settings = commonService.GetSettings();
            var filePath = settings["IMG_FILE_PATH"];
            var host = settings["IMG_FILE_HOST"];
            Int32.TryParse(settings["IMG_FILE_PORT"], out var port);
            var username = settings["IMG_FILE_USERNAME"];
            var password = settings["IMG_FILE_PASSWORD"];

            foreach (var item in movie.ImageList)
            {
                var isValidBase64 = ImageHelper.IsBase64Image(item.Base64Img, out var fileExtension);
                if (!isValidBase64) {
                    return OperationResult.Failed("Base64 is not valid");
                }
                item.FileExtension = fileExtension;
            }

            using var manager = new MovieManager();
            manager.BeginTransaction();

            var movieId = manager.InsertMovie(movie);

            foreach (var item in movie.GenreList)
            {
                item.MovieId = movieId;
                manager.InsertMovieGenre(item);
            }

            foreach (var item in movie.ImageList)
            {
                item.MovieId = movieId;
                var path = Path.Combine(filePath, $"MI{item.MovieId}.{item.FileExtension}");
                path = ImageHelper.SaveBase64Image(item.Base64Img, path, host, port, username, password);
                item.Path = Path.GetFileName(path);
                var imageId = manager.InsertMovieImages(item);
                item.ImageId = imageId;
            }

            foreach (var item in movie.ScheduleList)
            {
                item.MovieId = movieId;
                manager.InsertMovieSchedule(item);
            }

            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = loggonedUser.UserId,
                Description = $"Added new movie: {movie.Title}"
            });

            manager.Commit();

            return OperationResult.Success("Movie added successfully.");
        }


    }
}
