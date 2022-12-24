using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Utilities.attributes;

namespace WH.PORTAL.Models.RequestResponse
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Password]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [ContactNumber]
        public string PhoneNo { get; set; }


    }
}
