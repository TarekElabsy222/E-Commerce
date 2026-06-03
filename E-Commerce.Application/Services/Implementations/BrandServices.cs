using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Brand;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;

namespace E_Commerce.Application.Services.Implementations
{
    public class BrandServices : IBrandServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BrandServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions        
        public async Task<ServiceResponse> AddBrandAsync(CreateBrand brand)
        {
            var mappedData = _mapper.Map<Brand>(brand);
             await _unitOfWork.Brands.AddAsync(mappedData);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? new ServiceResponse(true, "Brand added!", mappedData)
                              : new ServiceResponse(false, "Brand failed to added");
        }

        public async Task<ServiceResponse> DeleteBrandAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid brand id");
            await _unitOfWork.Brands.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
        ? new ServiceResponse(true, "Brand deleted !")
        : new ServiceResponse(false, "Brand not found or failed to be deleted");
        }

        public async Task<IEnumerable<GetBrand>> GetAllBrandsAsync()
        {
            var rawData = await _unitOfWork.Brands.GetAllAsync();
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetBrand>>(rawData);
        }

        public async Task<GetBrand> GetBrandByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.Brands.GetSingleAsync(c => c.Id == id);
            return rawData is null ? null! : _mapper.Map<GetBrand>(rawData);
        }

        public async Task<GetBrand> GetBrandByNameAsync(string name)
        {
            var rawData = await _unitOfWork.Brands.GetSingleAsync(c => c.Name == name);
            return rawData is null ? null! : _mapper.Map<GetBrand>(rawData);
        }

        public async Task<ServiceResponse> UpdateBrandAsync(UpdateBrand brand)
        {
            var existingBrand = await _unitOfWork.Brands.GetSingleAsync(c => c.Id == brand.Id);
            if (existingBrand == null)
                return new ServiceResponse(false, "Brand not found");
            _mapper.Map(brand, existingBrand);
            await _unitOfWork.Brands.UpdateAsync(existingBrand);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                          ? new ServiceResponse(true, "Brand updated successfully!", existingBrand)
                          : new ServiceResponse(false, "Failed to update brand.");
        }
        #endregion
    }
}
