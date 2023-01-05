using DBHelper;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class SettingManager : BaseManager
    {
        #region SELECT
        public List<I_Settings> SelectSettingsList()
        {
            string sql = @$"SELECT * FROM I_SETTINGS";
            return SelectList<I_Settings>(sql);

        }
        #endregion


        #region UPDATE
        public void UpdateSetting(string settingCode, string settingVal)
        {
            string sql = @"UPDATE I_SETTINGS SET setting_val = @setting_val WHERE setting_code = @setting_code";
            AddParameter("@setting_code", settingCode);
            AddParameter("@setting_val", settingVal);
            ExecuteQuery(sql);
        }
        #endregion
    }
}
