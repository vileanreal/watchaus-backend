using WH.ADMIN.DBManager;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Services
{
    public class SmtpService
    {
        public SmtpSetting GetSmtpSetting(string useFor) {
            using SmtpManager manager = new SmtpManager();
            return manager.SelectSmtpSetting("use_for", useFor);
        }
    }
}
