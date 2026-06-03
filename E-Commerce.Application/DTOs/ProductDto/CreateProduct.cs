using Microsoft.AspNetCore.Http;

namespace E_Commerce.Application.DTOs.ProductDto
{
    public class CreateProduct : ProductBase
    {
        public IFormFile? ImageFile { get; set; }
    }
}
