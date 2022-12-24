using System.Security.Cryptography.X509Certificates;
using WH.ADMIN.Models.RequestResponse;

namespace WH.ADMIN.Models.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public long? BranchId { get; set; }
        public string BranchName { get; set; }
        public long? RoleId { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }


        public User() { }

        public User(AddUserRequest request)
        {
            this.Username = request.Username;
            this.FirstName = request.FirstName;
            this.MiddleName = request.MiddleName;
            this.LastName = request.LastName;
            this.Email = request.Email;
            this.PhoneNo = request.PhoneNo;
            this.BranchId = request.BranchId;
            this.RoleId = request.RoleId;
        }
    }
}
