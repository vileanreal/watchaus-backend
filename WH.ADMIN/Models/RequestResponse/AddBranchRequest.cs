using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class AddBranchRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
