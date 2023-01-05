using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Utilities.attributes
{
    public class ContactNumberAttribute : ValidationAttribute
    {
        private static readonly string pattern = @"^(\+63|0)\d{10}$";

        public ContactNumberAttribute()
        {
            ErrorMessage = "The contact number is not valid.";
        }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty(value as string))
            {
                return true;
            }

            string? contactNumber = value as string;
            if (contactNumber == null)
            {
                return false;
            }

            return Regex.IsMatch(contactNumber, pattern);
        }
    }
}
