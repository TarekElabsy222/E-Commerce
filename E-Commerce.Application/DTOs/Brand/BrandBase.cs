using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.Brand
{
    public class BrandBase
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
