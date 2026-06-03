using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CartItemsDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;

namespace E_Commerce.Application.Services.Implementations
{
    public class CartItemsServices : ICartItemsServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CartItemsServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<ServiceResponse> AddItemToCartAsync(CreateCartItems item)
        {
            var cart = await _unitOfWork.Carts.GetAllAsync(w => w.Id == item.CartId);

            if (!cart.Any()) return new ServiceResponse(false, "Cart not found.");

            var product = await _unitOfWork.Products.GetAllAsync(p => p.Id == item.ProductId);

            if (!product.Any()) return new ServiceResponse(false, "Product not found.");

            var mappedData = _mapper.Map<CartItems>(item);

            await _unitOfWork.CartItems.AddAsync(mappedData);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Cart item added!", mappedData)
                : new ServiceResponse(false, "Failed to add Cart item.");
        }

        public async Task<ServiceResponse> DeleteItemFromCartAsync(Guid id)
        {
            if (id == Guid.Empty) return new ServiceResponse(false, "Invalid Cart item id.");

            var item = await _unitOfWork.CartItems.GetSingleAsync(w => w.Id == id);

            if (item == null) return new ServiceResponse(false, "Cart item not found.");

            await _unitOfWork.CartItems.DeleteAsync(id);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Cart item deleted successfully!", item)
                : new ServiceResponse(false, "Failed to delete cart item.");
        }

        public async Task<IEnumerable<GetCartItems>> GetAllCartsItemsAsync()
        {
            var rawData = await _unitOfWork.CartItems.GetAllAsync(includeProperties: "Product");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetCartItems>>(rawData);
        }

        public async Task<GetCartItems> GetCartItemByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.CartItems.GetSingleAsync(r => r.Id == id, includeProperties: "Product");
            return rawData is null ? null! : _mapper.Map<GetCartItems>(rawData);
        }

        public async Task<IEnumerable<GetCartItems>> GetItemsInCartAsync(Guid cartId)
        {
            var list = await _unitOfWork.Carts.GetSingleAsync(r => r.Id == cartId);
            if (list is null) return [];
            var rawData = await _unitOfWork.CartItems.GetAllAsync(w => w.CartId == cartId, includeProperties: "Product");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetCartItems>>(rawData);
        }

        public async Task<ServiceResponse> UpdateCartItemAsync(UpdateCartItems item)
        {

            var existingItem = await _unitOfWork.CartItems.GetSingleAsync(w => w.Id == item.Id);

            if (existingItem == null) return new ServiceResponse(false, "Cart item not found.");

            var wishlist = await _unitOfWork.Carts.GetSingleAsync(w => w.Id == item.CartId);

            if (wishlist == null) return new ServiceResponse(false, "Cart not found.");


            var product = await _unitOfWork.Products.GetSingleAsync(p => p.Id == item.ProductId);

            if (product == null) return new ServiceResponse(false, "Product not found.");

            _mapper.Map(item, existingItem);

            await _unitOfWork.CartItems.UpdateAsync(existingItem);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Cart item updated successfully!", existingItem)
                : new ServiceResponse(false, "Failed to update cart item.");
        }
    }
    #endregion
}
