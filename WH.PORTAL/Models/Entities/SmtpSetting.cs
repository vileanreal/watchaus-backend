namespace WH.PORTAL.Models.Entities
{
    public class SmtpSetting
    {
        public long SmtpId { get; set; }
        public string UseFor { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsEnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderAddress { get; set; }
        public string SenderName { get; set; }
    }
}
