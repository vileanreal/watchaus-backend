namespace WH.ADMIN.Models.Entities
{
    public class UsersSercurityLogs
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string LogDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
