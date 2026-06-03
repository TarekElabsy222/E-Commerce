using Microsoft.AspNetCore.Http;

namespace E_Commerce.Application.DTOs.ProductDto
{
    public class UpdateProduct : ProductBase
    {
        public Guid Id { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
