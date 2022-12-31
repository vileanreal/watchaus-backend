using System.ComponentModel.DataAnnotations;

namespace WH.ADMIN.Models.RequestResponse
{
    public class UpdateSettingsRequest
    {
        [Required]
        public List<UpdateSettingRequestList> SettingList { get; set; }
    }

    public class UpdateSettingRequestList
    {
        [Required]
        public string SettingCode { get; set; }

        [Required]
        public string SettingVal { get; set; }
    }
}
