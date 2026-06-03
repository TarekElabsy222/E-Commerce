using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOs.ProductDto
{
    public class ProductBase
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        public int StockAmount { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public double? DiscountPercentage { get; set; }
    }
}
