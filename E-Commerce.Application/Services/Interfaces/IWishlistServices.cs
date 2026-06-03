using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.WishList;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IWishlistServices
    {
        public Task<GetWishlist> GetWishlistByIdAsync(Guid id);
        public Task<IEnumerable<GetWishlist>> GetAllWishlistAsync();
        public Task<ServiceResponse> AddWishlistAsync(CreateWishlist dto, Guid userId);
        public Task<ServiceResponse> UpdateWishlistAsync(UpdateWishlist dto);
        public Task<ServiceResponse> DeleteWishlistAsync(Guid id);
    }
}
