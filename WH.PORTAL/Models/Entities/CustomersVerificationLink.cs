namespace WH.PORTAL.Models.Entities
{
    public class CustomersVerificationLink
    {
        public long LinkId { get; set; }
        public long CustomerId { get; set; }
        public string LinkCode { get; set; }
        public DateTime DateCreated { get; set; }
        public string Purpose { get; set; }
        public string Status { get; set; }
    }
}
