using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.WishListItemsDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IWishlistItemServices
    {
        public Task<GetWishListItems> GetWishlistItemByIdAsync(Guid id);
        public Task<IEnumerable<GetWishListItems>> GetAllWishlistItemsAsync();
        public Task<IEnumerable<GetWishListItems>> GetItemsInWishlistAsync(Guid listId);

        public Task<ServiceResponse> AddWishlistItemAsync(CreateWishListItems item);
        public Task<ServiceResponse> UpdateWishlistItemAsync(Guid id, UpdateWishListItems item);
        public Task<ServiceResponse> DeleteWishlistItemAsync(Guid id);
    }
}
