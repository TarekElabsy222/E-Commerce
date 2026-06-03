using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.AccountDto
{
    public class PasswordSettingDto
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Not Matched!")]
        public string ConfirmPassword { get; set; }
    }
}
