using System.Net;
using System.Net.Mail;
using WH.PORTAL.Models.Entities;
using WH.PORTAL.Services;

namespace WH.PORTAL.Helper
{
    public class EmailHelper
    {
        private SmtpClient Client { get; set; }
        private SmtpSetting Setting { get; set; }

        public EmailHelper()
        {
            SmtpService service = new SmtpService();

            Setting = service.GetSmtpSetting("CUSTOMER");

            if (Setting == null)
            {
                throw new Exception("No defined smtp setting for customer.");
            }

            Client = new SmtpClient
            {
                Host = Setting.Host,
                Port = Setting.Port,
                EnableSsl = Setting.IsEnableSsl,
                Credentials = new NetworkCredential(Setting.Username, Setting.Password)
            };
        }

        public void SendEmail(string to, string subject, string body)
        {
            // Create a new MailMessage object
            var message = new MailMessage
            {
                From = new MailAddress(Setting.SenderAddress, Setting.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            // Add the recipient
            message.To.Add(to);

            // Send the email
            Client.Send(message);
        }
    }
}
