using System.ComponentModel.DataAnnotations;
using Utilities.attributes;

namespace WH.ADMIN.Models.RequestResponse
{
    public class UpdateUserRequest
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username must not contain any special characters.")]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [ContactNumber]
        public string PhoneNo { get; set; }
        public long? BranchId { get; set; }
        [Required]
        [RegularExpression("^(1|2)$", ErrorMessage = "Invalid RoleID value. 1=ADMIN, 2=OPERATOR")]
        public long RoleId { get; set; }
    }
}
