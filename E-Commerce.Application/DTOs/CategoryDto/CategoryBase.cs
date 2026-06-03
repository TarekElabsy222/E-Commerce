using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.CategoryDto
{
    public class CategoryBase
    {
        [Required]
        [MinLength(6)]
        public string? Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
