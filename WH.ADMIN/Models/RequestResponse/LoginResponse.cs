using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string BranchName { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }

        public LoginResponse() { }

        public LoginResponse(User user)
        {
            Token = ""; // generate token here
            Username = user.Username;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNo = user.PhoneNo;
            BranchName = user.BranchName;
            RoleName = user.RoleName;
            Status = user.Status;
        }
    }
}
