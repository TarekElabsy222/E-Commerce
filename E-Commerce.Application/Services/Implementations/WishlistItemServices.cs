using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Review;
using E_Commerce.Application.DTOs.WishListItemsDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;

namespace E_Commerce.Application.Services.Implementations
{
    public class WishlistItemServices : IWishlistItemServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public WishlistItemServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<ServiceResponse> AddWishlistItemAsync(CreateWishListItems item)
        {
            var wishlist = await _unitOfWork.Wishlists
                .GetAllAsync(w => w.Id == item.WishlistId);

            if (!wishlist.Any())
            {
                return new ServiceResponse(false, "Wishlist not found.");
            }

            var product = await _unitOfWork.Products
                .GetAllAsync(p => p.Id == item.ProductId);

            if (!product.Any())
            {
                return new ServiceResponse(false, "Product not found.");
            }

            var mappedData = _mapper.Map<WishlistItems>(item);

            await _unitOfWork.WishlistItems.AddAsync(mappedData);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Wishlist item added!", mappedData)
                : new ServiceResponse(false, "Failed to add wishlist item.");
        }

        public async Task<ServiceResponse> DeleteWishlistItemAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new ServiceResponse(false, "Invalid wishlist item id.");
            }

            var item = await _unitOfWork.WishlistItems
                .GetSingleAsync(w => w.Id == id);

            if (item == null)
            {
                return new ServiceResponse(false, "Wishlist item not found.");
            }

            await _unitOfWork.WishlistItems.DeleteAsync(id);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Wishlist item deleted successfully!", item)
                : new ServiceResponse(false, "Failed to delete wishlist item.");
        }

        public async Task<IEnumerable<GetWishListItems>> GetAllWishlistItemsAsync()
        {
            var rawData = await _unitOfWork.WishlistItems.GetAllAsync( includeProperties: "Wishlist,Product");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetWishListItems>>(rawData);
        }

        public async Task<IEnumerable<GetWishListItems>> GetItemsInWishlistAsync(Guid listId)
        {
            var list = await _unitOfWork.Wishlists.GetSingleAsync(r => r.Id == listId);
            if (list is null) return [];
            var rawData = await _unitOfWork.WishlistItems.GetAllAsync(w=>w.WishlistId == listId,includeProperties: "Wishlist,Product");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetWishListItems>>(rawData);
        }

        public async Task<GetWishListItems> GetWishlistItemByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.WishlistItems.GetSingleAsync(r => r.Id == id, includeProperties: "Wishlist,Product");
            return rawData is null ? null! : _mapper.Map<GetWishListItems>(rawData);
        }

        public async Task<ServiceResponse> UpdateWishlistItemAsync(Guid id, UpdateWishListItems item)
        {
            if (id == Guid.Empty) return new ServiceResponse(false, "Invalid wishlist item id.");


            var existingItem = await _unitOfWork.WishlistItems.GetSingleAsync(w => w.Id == id);

            if (existingItem == null) return new ServiceResponse(false, "Wishlist item not found.");

            var wishlist = await _unitOfWork.Wishlists.GetSingleAsync(w => w.Id == item.WishlistId);

            if (wishlist == null) return new ServiceResponse(false, "Wishlist not found.");
           

            var product = await _unitOfWork.Products.GetSingleAsync(p => p.Id == item.ProductId);

            if (product == null) return new ServiceResponse(false, "Product not found.");

            _mapper.Map(item, existingItem);

            await _unitOfWork.WishlistItems.UpdateAsync(existingItem);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Wishlist item updated successfully!", existingItem)
                : new ServiceResponse(false, "Failed to update wishlist item.");
        }
        #endregion
    }
}
