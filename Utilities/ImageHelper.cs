using Renci.SshNet;

namespace Utilities
{
    public static class ImageHelper
    {

        // file path example:
        // "sftp:///path/to/image.jpg"
        // "file:///path/to/image.jpg"
        // "file:///C:/path/to/image.jpg"
        // "C:\\path\\to\\image.jpg"
        // "\\\\server\\share\\path\\to\\image.jpg"
        public static string SaveBase64Image(string base64Image, string filePath, string host = null, int port = 0, string username = null, string password = null)
        {
            if (filePath.StartsWith("sftp://"))
            {
                return SaveBase64ImageToSftp(base64Image, filePath, host, port, username, password);
            }
            else
            {
                return SaveBase64ImageToLocalFileSystem(base64Image, filePath);
            }
        }


        private static string SaveBase64ImageToSftp(string base64Image, string filePath, string host, int port, string username, string password)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            // Save the image to an SFTP server
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Host must be specified for SFTP file path.");
            }
            if (port <= 0)
            {
                port = 22;
            }
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username must be specified for SFTP file path.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password must be specified for SFTP file path.");
            }

            using (SftpClient client = new SftpClient(host, port, username, password))
            {
                client.Connect();

                // Check if the file already exists
                if (client.Exists(filePath))
                {
                    // File already exists, so generate a new file name
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string fileExtension = Path.GetExtension(filePath);
                    string fileDirectory = Path.GetDirectoryName(filePath);
                    int counter = 1;
                    while (client.Exists(filePath))
                    {
                        filePath = Path.Combine(fileDirectory, $"{fileName} ({counter}){fileExtension}");
                        counter++;
                    }
                }

                // Save the image to the file
                using (Stream stream = client.Create(filePath))
                {
                    stream.Write(imageBytes, 0, imageBytes.Length);
                }
                client.Disconnect();
            }

            return filePath;
        }

        private static string SaveBase64ImageToLocalFileSystem(string base64Image, string filePath)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            // Check if the file already exists
            if (File.Exists(filePath))
            {
                // File already exists, so generate a new file name
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExtension = Path.GetExtension(filePath);
                string fileDirectory = Path.GetDirectoryName(filePath);
                int counter = 1;
                while (File.Exists(filePath))
                {
                    filePath = Path.Combine(fileDirectory, $"{fileName} ({counter}){fileExtension}");
                    counter++;
                }
            }

            // Save the image to the local file system
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {

                // Save the image to the file
                fs.Write(imageBytes, 0, imageBytes.Length);
            }

            return filePath;
        }



        public static string GetBase64Image(string filePath, string host = null, int port = 0, string username = null, string password = null)
        {
            if (filePath.StartsWith("sftp://"))
            {
                return GetBase64ImageFromSftp(filePath, host, port, username, password);
            }
            else
            {
                return GetBase64ImageFromLocalFileSystem(filePath);
            }
        }

        private static string GetBase64ImageFromSftp(string filePath, string host, int port, string username, string password)
        {
            // Retrieve the image from an SFTP server
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException("Host must be specified for SFTP file path.");
            }
            if (port <= 0)
            {
                port = 22;
            }
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username must be specified for SFTP file path.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password must be specified for SFTP file path.");
            }

            using (SftpClient client = new SftpClient(host, port, username, password))
            {
                client.Connect();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    client.DownloadFile(filePath, memoryStream);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
                client.Disconnect();
            }
        }

        private static string GetBase64ImageFromLocalFileSystem(string filePath)
        {
            // Retrieve the image from the local file system
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fs.CopyTo(memoryStream);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }




        public static bool IsBase64Image(string base64, out string extension)
        {
            extension = String.Empty;
            Dictionary<string, byte[]> imageFormats = new Dictionary<string, byte[]>
            {
                { "jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
                { "png", new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } }
            };

            byte[] data = null;

            try
            {
                data = Convert.FromBase64String(base64);
            }
            catch (Exception ex)
            {
                return false;
            }

            foreach (var kvp in imageFormats)
            {
                if (data.Take(kvp.Value.Length).SequenceEqual(kvp.Value))
                {
                    // the base64 string is a supported image format
                    extension = kvp.Key;
                    return true;
                }
            }

            // the base64 string is not a supported image format
            return false;
        }

    }
}
