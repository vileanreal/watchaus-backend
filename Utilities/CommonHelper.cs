using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utilities
{
    public class CommonHelper
    {
        public static string GenerateEmailVerificationCode()
        {
            // Generate a random, unique code using the Guid class
            string code = Guid.NewGuid().ToString("N");

            // Hash the code using a secure hashing algorithm (e.g. SHA256)
            using (var hashAlgorithm = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(code));
                code = Convert.ToBase64String(hash);
            }

            // Truncate the hashed code to the desired length (e.g. 10 characters)
            code = code.Substring(0, 10);
            // Return the code
            return code;
        }

        public static bool IsMilitaryTime(string time)
        {
            return Regex.IsMatch(time, @"^([01]\d|2[0-3]):([0-5]\d)$");
        }
    }
}
