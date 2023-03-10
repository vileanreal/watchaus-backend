using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class AddMovieRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public int Duration { get; set; }
        [Required]
        public List<int> Genres { get; set; }
        [Required]
        public string MoviePosterImg { get; set; }
        [Required]
        public List<string> Screenshots { get; set; }

        [Required]
        public string showingDateStart { get; set; }
        [Required]
        public string showingDateEnd { get; set; }
    }
}
