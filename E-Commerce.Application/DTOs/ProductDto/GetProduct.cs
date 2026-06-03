namespace E_Commerce.Application.DTOs.ProductDto
{
    public class GetProduct : ProductBase
    {
        public Guid Id { get; set; }
        public string? Image { get; set; }
        public string? CategoryName { get; set; }
        public string? BrandName { get; set; }
        public decimal? AfterDiscount { get; set; }
    }
}
