namespace WH.ADMIN.Models.RequestResponse
{
    public class GetAssignedMovieListResponse
    {
        public long SamId { get; set; }
        public long ScreenId { get; set; }
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string MoviePosterImg { get; set; }
    }
}
