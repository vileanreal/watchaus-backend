using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetMovieDetailsResponse
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Duration { get; set; }
        public List<MovieGenreResponse> Genres { get; set; }
        public List<MovieScheduleResponse> Schedules { get; set; }
        public string MoviePosterBase64 { get; set; }
        public List<MovieScreenshotsResponse> Screenshots { get; set; }


        public GetMovieDetailsResponse(Movies movie)
        {
            this.MovieId = movie.MovieId;
            this.Title = movie.Title;
            this.Description = movie.Description;
            this.Duration = movie.Duration;
            this.Genres = movie.GenreList.Select(x => new MovieGenreResponse()
            {
                GenreId = x.GenreId,
                Name = x.Name,
            }).ToList();
            this.Schedules = movie.ScheduleList.Select(x => new MovieScheduleResponse()
            {
                ShowingDateStart = x.DateStart,
                ShowingDateEnd = x.DateEnd
            }).ToList();
            this.MoviePosterBase64 = movie.ImageList.Find(x => x.IsMoviePoster)?.Base64Img ?? "";
            this.Screenshots = movie.ImageList.FindAll(x => !x.IsMoviePoster).Select(x => new MovieScreenshotsResponse()
            {
                ImageId = x.ImageId,
                ImageBase64 = x.Base64Img
            }).ToList();

        }
    }


    public class MovieGenreResponse
    {
        public long GenreId { get; set; }
        public string Name { get; set; }
    }

    public class MovieScheduleResponse
    {
        public string ShowingDateStart { get; set; }
        public string ShowingDateEnd { get; set; }
    }

    public class MovieScreenshotsResponse
    {
        public long ImageId { get; set; }
        public string ImageBase64 { get; set; }
    }
}
