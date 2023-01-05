using Org.BouncyCastle.Asn1.Ocsp;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetUserDetailsResponse
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public long? BranchId { get; set; }
        public string BranchName { get; set; }
        public long? RoleId { get; set; }
        public string RoleName { get; set; }

        public GetUserDetailsResponse(User user) {
            this.UserId = user.UserId;
            this.Username = user.Username;
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.PhoneNo = user.PhoneNo;
            this.BranchId = user.BranchId;
            this.BranchName = user.BranchName;
            this.RoleId = user.RoleId;
            this.RoleName = user.RoleName;
        }
    }
}
