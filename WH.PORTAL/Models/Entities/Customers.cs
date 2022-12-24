using WH.PORTAL.Models.RequestResponse;

namespace WH.PORTAL.Models.Entities
{
    public class Customers
    {
        public long CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }

        public Customers() { }
        public Customers(RegisterRequest request)
        {
            Email = request.Email;
            Password = request.Password;
            FirstName = request.FirstName;
            MiddleName = request.MiddleName;
            LastName = request.LastName;
            PhoneNo = request.PhoneNo;
        }
    }
}
