using DBHelper;
using WH.PORTAL.Models.Entities;

namespace WH.PORTAL.DBManager
{
    public class CommonManager: BaseManager
    {
        #region SELECT
        public I_Settings SelectSetting(string settingCode)
        {
            string sql = @$"SELECT * FROM I_SETTINGS WHERE setting_code = @setting_code";
            AddParameter("@setting_code", settingCode);
            return SelectSingle<I_Settings>(sql);
        }

        public EmailTemplates SelectEmailTemplate(string templateName)
        {
            string sql = @$"SELECT * FROM EMAIL_TEMPLATES WHERE template_name = @template_name";
            AddParameter("@template_name", templateName);
            return SelectSingle<EmailTemplates>(sql);
        }
        #endregion

        #region INSERT
        #endregion

        #region UPDATE
        #endregion

        #region DELETE
        #endregion
    }
}
