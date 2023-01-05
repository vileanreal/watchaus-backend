using System.ComponentModel.DataAnnotations;

namespace Utilities.attributes
{

    public class PasswordAttribute : ValidationAttribute
    {
        public int MinLength { get; set; } = 8;
        public int MaxLength { get; set; } = int.MaxValue;
        public bool RequireLowercase { get; set; } = true;
        public bool RequireUppercase { get; set; } = true;
        public bool RequireDigit { get; set; } = true;
        public bool RequireSpecialCharacter { get; set; } = true;

        public PasswordAttribute()
        {
            ErrorMessage = "The password is not valid.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string password = value as string;
            if (password == null)
            {
                return false;
            }

            // Check length
            if (password.Length < MinLength)
            {
                ErrorMessage = $"The password must be at least {MinLength} characters long.";
                return false;
            }
            if (password.Length > MaxLength)
            {
                ErrorMessage = $"The password cannot be more than {MaxLength} characters long.";
                return false;
            }

            // Check for lowercase characters
            if (RequireLowercase && !password.Any(c => char.IsLower(c)))
            {
                ErrorMessage = "The password must contain at least one lowercase character.";
                return false;
            }

            // Check for uppercase characters
            if (RequireUppercase && !password.Any(c => char.IsUpper(c)))
            {
                ErrorMessage = "The password must contain at least one uppercase character.";
                return false;
            }

            // Check for digits
            if (RequireDigit && !password.Any(c => char.IsDigit(c)))
            {
                ErrorMessage = "The password must contain at least one digit.";
                return false;
            }

            // Check for special characters
            if (RequireSpecialCharacter && !password.Any(c => !char.IsLetterOrDigit(c)))
            {
                ErrorMessage = "The password must contain at least one special character.";
                return false;
            }

            return true;
        }
    }
}
