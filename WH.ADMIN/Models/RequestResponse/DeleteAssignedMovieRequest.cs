using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class DeleteAssignedMovieRequest
    {
        [Required]
        public long? ScreenId { get; set; }
        [Required]
        public long? MovieId { get; set; }
    }
}
