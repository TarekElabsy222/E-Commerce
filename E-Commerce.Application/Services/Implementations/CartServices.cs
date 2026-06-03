using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CartDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;

namespace E_Commerce.Application.Services.Implementations
{
    public class CartServices : ICartServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CartServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handle functions
        public async Task<ServiceResponse> AddCartAsync(Guid currentUser,CreateCart cart)
        {
            var mappedData = _mapper.Map<Cart>(cart);
            mappedData.CustomerId = currentUser;
            await _unitOfWork.Carts.AddAsync(mappedData);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? new ServiceResponse(true, "Cart added!", mappedData)
                              : new ServiceResponse(false, "Cart failed to added");
        }

        public async Task<ServiceResponse> DeleteCartAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid cart id");

            var cart = await _unitOfWork.Carts.GetSingleAsync(p => p.Id == id);
            if (cart is null) return new ServiceResponse(false, "Cart not found");
            await _unitOfWork.Carts.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Cart deleted successfully!")
                : new ServiceResponse(false, "Failed to delete carts");
        }

        public async Task<IEnumerable<GetCart>> GetAllCartsAsync()
        {
            var rawData = await _unitOfWork.Carts.GetAllAsync();
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetCart>>(rawData);
        }

        public async Task<GetCart> GetCartByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.Carts.GetSingleAsync(c => c.Id == id);
            return rawData is null ? null! : _mapper.Map<GetCart>(rawData);
        }

        public async Task<ServiceResponse> UpdateCartAsync(Guid id, UpdateCart cart)
        {
            var existingCart = await _unitOfWork.Carts.GetSingleAsync(r => r.Id == id);
            if (existingCart == null)
                return new ServiceResponse(false, "Cart not found");


            _mapper.Map(cart, existingCart);
            await _unitOfWork.Carts.UpdateAsync(existingCart);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                          ? new ServiceResponse(true, "Cart updated successfully!", existingCart)
                          : new ServiceResponse(false, "Failed to update cart.");
        }
        #endregion
    }
}
