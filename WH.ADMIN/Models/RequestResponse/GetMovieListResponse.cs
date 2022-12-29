using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetMovieListResponse
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Duration { get; set; }

        public GetMovieListResponse(Movies movie) {
            this.MovieId = movie.MovieId;
            this.Title = movie.Title;
            this.Description = movie.Description;
            this.Duration = movie.Duration;
        }
    }
}
