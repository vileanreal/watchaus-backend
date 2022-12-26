namespace WH.ADMIN.Models.RequestResponse
{
    public class GetUserListResponse
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
    }
}
