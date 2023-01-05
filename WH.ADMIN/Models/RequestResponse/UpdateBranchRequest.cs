using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class UpdateBranchRequest
    {
        [Required]
        public long? BranchId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
