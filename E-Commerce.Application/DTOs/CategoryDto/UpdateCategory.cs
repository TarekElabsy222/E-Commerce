using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.CategoryDto
{
    public class UpdateCategory : CategoryBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
