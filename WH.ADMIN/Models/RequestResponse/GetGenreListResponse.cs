using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetGenreListResponse
    {
        public long GenreId { get; set; }
        public string Name { get; set; }

        public GetGenreListResponse(I_Genres genre)
        {
            this.GenreId = genre.GenreId;
            this.Name = genre.Name;
        }
    }
}
