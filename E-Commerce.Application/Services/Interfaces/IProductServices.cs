using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.ProductDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<GetProduct>> GetAllProductsAsync();
        Task<GetProduct> GetProductByIdAsync(Guid id);
        Task<IEnumerable<GetProduct>> GetProductsByNameAsync(string productName);
        Task<IEnumerable<GetProduct>> GetProductByCategoryIdAsync(Guid categoryId);
        Task<IEnumerable<GetProduct>> GetProductsByCategoryNameAsync(string categoryName);
        Task<IEnumerable<GetProduct>> GetProductByBrandIdAsync(Guid brandId);
        Task<IEnumerable<GetProduct>> GetProductsByBrandNameAsync(string brandName);
        Task<ServiceResponse> AddProductAsync(CreateProduct product);
        Task<ServiceResponse> UpdateProductAsync(UpdateProduct product);
        Task<ServiceResponse> DeleteProductAsync(Guid id);
    }
}
