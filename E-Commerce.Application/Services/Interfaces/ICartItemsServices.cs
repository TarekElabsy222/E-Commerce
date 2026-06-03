using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CartItemsDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface ICartItemsServices
    {
        Task<IEnumerable<GetCartItems>> GetAllCartsItemsAsync();
        public Task<GetCartItems> GetCartItemByIdAsync(Guid id);
        Task<IEnumerable<GetCartItems>> GetItemsInCartAsync(Guid cartId);

        Task<ServiceResponse> AddItemToCartAsync(CreateCartItems item);
        Task<ServiceResponse> UpdateCartItemAsync(UpdateCartItems item);
        Task<ServiceResponse> DeleteItemFromCartAsync(Guid id);
    }
}
