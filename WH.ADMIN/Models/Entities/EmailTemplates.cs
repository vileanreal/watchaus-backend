namespace WH.ADMIN.Models.Entities
{
    public class EmailTemplates
    {
        public long TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
