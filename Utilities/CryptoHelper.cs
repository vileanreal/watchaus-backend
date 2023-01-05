using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public static class CryptoHelper
    {

        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("ThisIsAVerySecureSalt");

        public static string Encrypt(string text, string? password = "ThisIsAVerySecurePassword")
        {
            byte[] encryptedBytes;
            byte[] textBytes = Encoding.UTF8.GetBytes(text);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Salt);
                aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(textBytes, 0, textBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            encryptedBytes = memoryStream.ToArray();
                        }
                    }
                }
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string encryptedText, string? password = "ThisIsAVerySecurePassword")
        {
            byte[] textBytes;
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Salt);
                aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
                aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            textBytes = new byte[encryptedBytes.Length];
                            int decryptedByteCount = cryptoStream.Read(textBytes, 0, textBytes.Length);
                            Array.Resize(ref textBytes, decryptedByteCount);
                        }
                    }
                }
            }

            return Encoding.UTF8.GetString(textBytes);
        }
    }



    public static class PasswordHelper
    {
        private static readonly RNGCryptoServiceProvider _random = new RNGCryptoServiceProvider();

        public static int MinLength { get; set; } = 8;
        public static int MaxLength { get; set; } = int.MaxValue;
        public static bool RequireLowercase { get; set; } = true;
        public static bool RequireUppercase { get; set; } = true;
        public static bool RequireDigit { get; set; } = true;
        public static bool RequireSpecialCharacter { get; set; } = false;

        public static string GeneratePassword()
        {
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;':,.<>?";

            string validChars = "";
            if (RequireLowercase)
            {
                validChars += lowercaseChars;
            }
            if (RequireUppercase)
            {
                validChars += uppercaseChars;
            }
            if (RequireDigit)
            {
                validChars += digitChars;
            }
            if (RequireSpecialCharacter)
            {
                validChars += specialChars;
            }

            //int length = _random.Next(MinLength, MaxLength + 1);
            int length = MinLength;
            char[] chars = new char[length];
            byte[] data = new byte[length];

            _random.GetBytes(data);

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[data[i] % validChars.Length];
            }

            return new string(chars);
        }
    }
}
