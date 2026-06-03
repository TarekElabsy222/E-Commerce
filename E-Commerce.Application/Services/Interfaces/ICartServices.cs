using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CartDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface ICartServices
    {
        Task<GetCart> GetCartByIdAsync(Guid id);
        Task<IEnumerable<GetCart>> GetAllCartsAsync();

        Task<ServiceResponse> AddCartAsync(Guid currentUser,CreateCart cart);
        Task<ServiceResponse> UpdateCartAsync(Guid id,UpdateCart cart);
        Task<ServiceResponse> DeleteCartAsync(Guid id);
    }
}
