using System.ComponentModel.DataAnnotations;

namespace WH.PORTAL.Models.RequestResponse
{
    public class ConfirmEmailRequest
    {
        [Required]
        public long CustomerId { get; set; }
        [Required]
        public string LinkCode { get; set; }
    }
}
