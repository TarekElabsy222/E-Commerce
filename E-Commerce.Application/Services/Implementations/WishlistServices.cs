using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.WishList;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;

namespace E_Commerce.Application.Services.Implementations
{
    public class WishlistServices : IWishlistServices
    {

        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public WishlistServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<ServiceResponse> AddWishlistAsync(CreateWishlist dto, Guid userId)
        {
            var mappedData = _mapper.Map<Wishlist>(dto);
            mappedData.CustomerId = userId;
            await _unitOfWork.Wishlists.AddAsync(mappedData);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? new ServiceResponse(true, "Success added to wishlist!", mappedData)
                              : new ServiceResponse(false, "Failed to add wishlist!");
        }

        public async Task<ServiceResponse> DeleteWishlistAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid Wishlist id");
            await _unitOfWork.Wishlists.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
        ? new ServiceResponse(true, "Success Deleted to wishlist!")
        : new ServiceResponse(false, "Failed to delete wishlist");
        }

        public async Task<IEnumerable<GetWishlist>> GetAllWishlistAsync()
        {
            var rawData = await _unitOfWork.Wishlists.GetAllAsync();
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetWishlist>>(rawData);
        }

        public async Task<GetWishlist> GetWishlistByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.Wishlists.GetSingleAsync(c => c.Id == id);
            return rawData is null ? null! : _mapper.Map<GetWishlist>(rawData);
        }

        public async Task<ServiceResponse> UpdateWishlistAsync(UpdateWishlist dto)
        {
            var existingWishList = await _unitOfWork.Wishlists.GetSingleAsync(c => c.Id == dto.Id);
            if (existingWishList == null)
                return new ServiceResponse(false, "Wishlist not found");
            _mapper.Map(dto, existingWishList);
            await _unitOfWork.Wishlists.UpdateAsync(existingWishList);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                          ? new ServiceResponse(true, "Wishlist updated successfully!", existingWishList)
                          : new ServiceResponse(false, "Failed to update wishlist.");
        }
        #endregion
    }
}
