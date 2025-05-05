using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MapTask.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class PasswordComplexityAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password)) return false;

            bool hasUpper = Regex.IsMatch(password, "[A-Z]");
            bool hasNumber = Regex.IsMatch(password, "[0-9]");
            bool hasSpecial = Regex.IsMatch(password, "[^a-zA-Z0-9]");
            bool hasLength = password.Length >= 6;

            return hasUpper && hasNumber && hasSpecial && hasLength;
        }
    }
}
