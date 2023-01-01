using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class UploadMoviePosterRequest
    {
        [Required]
        public long? MovieId { get; set; }
        [Required]
        public string MoviePosterImg { get; set; }
    }
}
