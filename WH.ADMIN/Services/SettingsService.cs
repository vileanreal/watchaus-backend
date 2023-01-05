using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Models.Entities;
using WH.PORTAL.Services;

namespace WH.ADMIN.Services
{
    public class SettingsService
    {
        public List<I_Settings> GetSettingsList()
        {
            using var manager = new SettingManager();
            return manager.SelectSettingsList();
        }


        public OperationResult UpdateSettings(List<I_Settings> settingList, Session session)
        {
            var commonService = new CommonService();
            using var manager = new SettingManager();
            manager.BeginTransaction();
            var settings = new List<string>();
            foreach (var setting in settingList)
            {
                manager.UpdateSetting(setting.SettingCode, setting.SettingVal);
                settings.Add($"{setting.SettingCode} = {setting.SettingVal}");
            }

            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Updated settings: {string.Join(", ", settings)}"
            });
            manager.Commit();
            return OperationResult.Success("Settings updated successfully");
        }
    }
}
