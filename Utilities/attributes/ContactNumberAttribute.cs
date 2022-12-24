using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

            string contactNumber = value as string;
            if (contactNumber == null)
            {
                return false;
            }

            return Regex.IsMatch(contactNumber, pattern);
        }
    }
}
