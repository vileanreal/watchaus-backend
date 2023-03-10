using WH.PORTAL.DBManager;
using WH.PORTAL.Models.Entities;

namespace WH.PORTAL.Services
{
    public class CommonService
    {
        public I_Settings GetSetting(string settingCode)
        {
            using CommonManager manager = new CommonManager();
            var setting = manager.SelectSetting(settingCode);
            if (setting == null)
            {
                throw new Exception("Setting doesn't exist");
            }
            return setting;
        }

        public EmailTemplates GetEmailTemplate(string templateName)
        {
            using CommonManager manager = new CommonManager();
            var template = manager.SelectEmailTemplate(templateName);
            if (template == null)
            {
                throw new Exception("Template doesn't exist");
            }
            return template;
        }
    }
}
