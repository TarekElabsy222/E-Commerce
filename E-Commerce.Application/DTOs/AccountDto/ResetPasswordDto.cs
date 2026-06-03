using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.AccountDto
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Not Matched!")]
        public string ConfirmPassword { get; set; }
    }
}
