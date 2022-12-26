namespace WH.ADMIN.Models.Entities
{
    public class AuditTrails
    {
        public long AuditId { get; set; }
        public long UserId { get; set; }
        public string LogDate { get; set; }
        public string Description { get; set; }
    }
}
