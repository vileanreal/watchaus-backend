using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class UpdateMovieRequest
    {
        [Required]
        public long? MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public int Duration { get; set; }
        [Required]
        public List<int> Genres { get; set; }
    }
}
