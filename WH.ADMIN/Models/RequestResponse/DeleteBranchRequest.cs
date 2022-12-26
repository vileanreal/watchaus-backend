using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class DeleteBranchRequest
    {
        [Required]
        public long? BranchId { get; set; }
    }
}
