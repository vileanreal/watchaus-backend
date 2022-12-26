using DBHelper;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class CommonManager: BaseManager
    {
        #region SELECT

        public Dictionary<string,string> SelectAllSettings() {
            string sql = @$"SELECT * FROM I_SETTINGS";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            using var reader = ExecuteReader(sql);
            while (reader.Read())
            {
                var key = reader["setting_code"].ToString();
                var value = reader["setting_val"].ToString();
                dictionary[key] = value;
            }
            return dictionary;
        }

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

        public void InsertAuditTrails(AuditTrails audit) {
            string sql = @"INSERT INTO AUDIT_TRAILS (user_id, log_date, description)
                                              VALUES(@user_id, NOW(), @description)";
            AddParameter("@user_id", audit.UserId);
            AddParameter("@description", audit.Description);
            ExecuteQuery(sql);
            return;
        }
        #endregion

        #region UPDATE
        #endregion

        #region DELETE
        #endregion
    }
}
