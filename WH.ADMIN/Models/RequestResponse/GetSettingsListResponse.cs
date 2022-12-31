using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetSettingsListResponse
    {
        public string SettingCode { get; set; }
        public string SettingVal { get; set; }
        public string SettingDesc { get; set; }

        public GetSettingsListResponse(I_Settings i_settings)
        {
            SettingCode = i_settings.SettingCode;
            SettingVal = i_settings.SettingVal;
            SettingDesc = i_settings.SettingDesc;
        }
    }
}
