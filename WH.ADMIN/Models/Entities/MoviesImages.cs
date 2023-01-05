namespace WH.ADMIN.Models.Entities
{
    public class MoviesImages
    {
        public long ImageId { get; set; }
        public long MovieId { get; set; }
        public string Path { get; set; }
        public bool IsMoviePoster { get; set; }
        public string Base64Img { get; set; }
        public string FileExtension { get; set; }
    }
}
