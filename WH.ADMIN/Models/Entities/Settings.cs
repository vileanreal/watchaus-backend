namespace WH.ADMIN.Models.Entities
{
    public class Settings
    {
        public string ImgFilePath { get; set; }
        public string ImgFileHost { get; set; }
        public int ImgFilePort { get; set; }
        public string ImgFileUsername { get; set; }
        public string ImgFilePassword { get; set; }

        public Settings(Dictionary<string, string> dict) {
            ImgFilePath = dict["IMG_FILE_PATH"];
            ImgFileHost = dict["IMG_FILE_HOST"];
            Int32.TryParse(dict["IMG_FILE_PORT"], out var port);
            ImgFilePort = port;
            ImgFileUsername = dict["IMG_FILE_USERNAME"];
            ImgFilePassword = dict["IMG_FILE_PASSWORD"];
        }
    }
}
