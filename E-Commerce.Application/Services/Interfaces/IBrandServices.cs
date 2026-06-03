using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Brand;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IBrandServices
    {
        Task<IEnumerable<GetBrand>> GetAllBrandsAsync();
        Task<GetBrand> GetBrandByIdAsync(Guid id);
        Task<GetBrand> GetBrandByNameAsync(string name);
        Task<ServiceResponse> AddBrandAsync(CreateBrand brand);
        Task<ServiceResponse> UpdateBrandAsync(UpdateBrand brand);
        Task<ServiceResponse> DeleteBrandAsync(Guid id);
    }
}
