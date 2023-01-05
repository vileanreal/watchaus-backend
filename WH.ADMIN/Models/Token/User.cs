using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.Token
{
    public class UserDetails
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }

        public UserDetails(User user)
        {
            UserId = user.UserId.ToString() ?? "";
            Username = user.Username ?? "";
            RoleId = user.RoleId?.ToString() ?? "";
            RoleName = user.RoleName ?? "";
            Email = user.Email ?? "";
        }
    }
}
