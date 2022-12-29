namespace WH.ADMIN.Models.Entities
{
    public class MoviesGenre
    {
        public long MgId { get; set; }
        public long MovieId { get; set; }
        public long GenreId { get; set; }
        public string Name { get; set; }
    }
}
