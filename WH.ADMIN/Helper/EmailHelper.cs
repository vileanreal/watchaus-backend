using System.Net;
using System.Net.Mail;
using Utilities.Exceptions;
using WH.ADMIN.Models.Entities;
using WH.ADMIN.Services;

namespace WH.ADMIN.Helper
{
    public class EmailHelper
    {
        private SmtpClient Client { get; set; }
        private SmtpSetting Setting { get; set; }

        public EmailHelper()
        {
            SmtpService service = new SmtpService();
            Setting = service.GetSmtpSetting("INTERNAL");

            if (Setting == null)
            {
                throw new NullResultException("No defined smtp setting for internal users.");
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
