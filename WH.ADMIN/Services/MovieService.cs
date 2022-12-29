using System.Security.Cryptography.X509Certificates;
using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;
using WH.PORTAL.Services;

namespace WH.ADMIN.Services
{
    public class MovieService
    {

        public List<Movies> GetMovieList() {
            using var manager = new MovieManager();
            return manager.SelectMovieList();
        }

        public Movies GetMovieById(long movieId) { 
            using var manager = new MovieManager();
            return manager.SelectMovie("movie_id", movieId.ToString());
        }

        public bool IsMovieExist(long movieId) { 
            var movie = GetMovieById(movieId);
            return movie != null;
        }

        public OperationResult AddMovie(Movies movie, Session session) {
            var commonService = new CommonService();

            var settings = new Settings(commonService.GetSettings());

            foreach (var item in movie.ImageList)
            {
                var isValidBase64 = ImageHelper.IsBase64Image(item.Base64Img, out var fileExtension);
                if (!isValidBase64) {
                    return OperationResult.Failed("Base64 is not valid.");
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
                var path = Path.Combine(settings.ImgFilePath, $"MI{item.MovieId}.{item.FileExtension}");
                path = ImageHelper.SaveBase64Image(item.Base64Img, path,
                                                   settings.ImgFileHost, 
                                                   settings.ImgFilePort, 
                                                   settings.ImgFileUsername, 
                                                   settings.ImgFilePassword);
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
                UserId = session.Id,
                Description = $"Added new movie: {movie.Title}"
            });

            manager.Commit();

            return OperationResult.Success("Movie added successfully.");
        }

        public OperationResult UpdateMovie(Movies movie, Session session) { 
            var commonService = new CommonService();

            
            if (!IsMovieExist(movie.MovieId)) {
                return OperationResult.Failed("Movie doesn't exist.");
            }


            using var manager = new MovieManager();
            manager.BeginTransaction();

            manager.UpdateMovieDetails(movie);
            manager.DeleteMovieGenres(movie.MovieId);

            foreach (var item in movie.GenreList)
            {
                manager.InsertMovieGenre(item);
            }
            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Updated movie details: {movie.MovieId}"
            });
            manager.Commit();

            return OperationResult.Success("Movie updated successfully");
        }


        public OperationResult DeleteMovie(long movieId, Session session) {
            var commonService = new CommonService();

            if (!IsMovieExist(movieId))
            {
                return OperationResult.Failed("Movie doesn't exist.");
            }

            using var manager = new MovieManager();
            manager.BeginTransaction();

            manager.UpdateMovieStatus(movieId, Status.DELETED);

            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Deleted movie: {movieId}"
            });

            manager.Commit();

            return OperationResult.Success("Movie deleted successfully.");
        }




        public Movies GetMovieDetails(long movieId) {

            var movie = GetMovieById(movieId);

            if (movie == null) {
                return null;
            }

            using (var manager = new MovieManager()) {
                movie.GenreList = manager.SelectMovieGenre(movieId);
                movie.ImageList = manager.SelectMovieImages(movieId);
                movie.ScheduleList = manager.SelectMovieSchedule(movieId);
            }

            var commonService = new CommonService();
            var settings = new Settings(commonService.GetSettings());

            foreach (var item in movie.ImageList) {
                var filePath = Path.Combine(settings.ImgFilePath, item.Path);
                item.Base64Img = ImageHelper.GetBase64Image(filePath,
                                                            settings.ImgFileHost,
                                                            settings.ImgFilePort,
                                                            settings.ImgFileUsername,
                                                            settings.ImgFilePassword);
            }

            return movie;
        }

    }
}
