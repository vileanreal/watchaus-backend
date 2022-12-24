using WH.PORTAL.DBManager;
using WH.PORTAL.Models.Entities;

namespace WH.PORTAL.Services
{
    public class SmtpService
    {
        public SmtpSetting GetSmtpSetting(string useFor) {
            using SmtpManager manager = new SmtpManager();
            return manager.SelectSmtpSetting("use_for", useFor);
        }
    }
}
