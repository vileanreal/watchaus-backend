

using WH.ADMIN.Models.RequestResponse;

namespace WH.ADMIN.Models.Entities
{
    public class I_Settings
    {
        public string SettingCode { get; set; }
        public string SettingVal { get; set; }
        public string SettingDesc { get; set; }

        public I_Settings()
        {

        }

        public I_Settings(UpdateSettingRequestList request)
        {
            SettingCode = request.SettingCode;
            SettingVal = request.SettingVal;
        }
    }
}
