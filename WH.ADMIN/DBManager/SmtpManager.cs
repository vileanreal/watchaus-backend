using DBHelper;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class SmtpManager : BaseManager
    {

        #region SELECT
        public SmtpSetting SelectSmtpSetting(string col, string val)
        {
            string sql = @$"SELECT * FROM SMTP_SETTING WHERE {col} = @Val";
            AddParameter("@Val", val);
            SmtpSetting setting = SelectSingle<SmtpSetting>(sql);
            return setting;
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
