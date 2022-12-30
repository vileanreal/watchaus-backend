using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class UpdateScreenRequest
    {
        [Required]
        public long? ScreenId { get; set; }
        [Required]
        public long? BranchId { get; set; }
        [Required]
        public string ScreenName { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public int? NoOfSeats { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public Decimal? Charge { get; set; }
        [Required]
        public List<string> ShowTimes { get; set; }
    }
}
