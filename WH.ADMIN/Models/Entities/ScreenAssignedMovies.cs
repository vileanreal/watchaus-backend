using WH.ADMIN.Models.RequestResponse;

namespace WH.ADMIN.Models.Entities
{
    public class ScreenAssignedMovies
    {
        public long SamId { get; set; }
        public long MovieId { get; set; }
        public long ScreenId { get; set; }
        public string status { get; set; }

        public ScreenAssignedMovies() { }

        public ScreenAssignedMovies(AssignMovieToScreenRequest request)
        {
            MovieId = request.MovieId ?? 0;
            ScreenId = request.ScreenId ?? 0;
        }
        public ScreenAssignedMovies(DeleteAssignedMovieRequest request)
        {
            MovieId = request.MovieId ?? 0;
            ScreenId = request.ScreenId ?? 0;
        }
    }
}
