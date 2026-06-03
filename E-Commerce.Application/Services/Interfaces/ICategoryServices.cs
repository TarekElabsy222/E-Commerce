using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CategoryDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<IEnumerable<GetCategory>> GetAllCategoriesAsync();
        Task<GetCategory> GetCategoryByIdAsync(Guid id);
        Task<GetCategory> GetCategoryByNameAsync(string name);
        Task<ServiceResponse> AddCategoryAsync(CreateCategory category);
        Task<ServiceResponse> UpdateCategoryAsync(UpdateCategory category);
        Task<ServiceResponse> DeleteCategoryAsync(Guid id);
    }
}
