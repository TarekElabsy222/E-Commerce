using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CategoryDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;

namespace E_Commerce.Application.Services.Implementations
{
    public class CategoryServices : ICategoryServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public CategoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions        
        public async Task<ServiceResponse> AddCategoryAsync(CreateCategory category)
        {
            var mappedData = _mapper.Map<Category>(category);
            // check parent category id is exist
            if (mappedData.ParentCategoryId.HasValue)
            {
                var parentCategory = await _unitOfWork.Categories.GetSingleAsync(c => c.Id == mappedData.ParentCategoryId.Value);
                if (parentCategory == null) 
                    return new ServiceResponse(false, "parent category does not exist.");
            }
             await _unitOfWork.Categories.AddAsync(mappedData);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? new ServiceResponse(true, "Category added!", mappedData)
                              : new ServiceResponse(false, "Category failed to added");
        }

        public async Task<ServiceResponse> DeleteCategoryAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid category id");
            var children = await _unitOfWork.Categories.GetAllAsync(c => c.ParentCategoryId == id);
            foreach (var child in children)
                await _unitOfWork.Categories.DeleteAsync(child.Id);
            await _unitOfWork.Categories.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
        ? new ServiceResponse(true, "Category deleted along with its children!")
        : new ServiceResponse(false, "Category not found or failed to be deleted");
        }

        public async Task<IEnumerable<GetCategory>> GetAllCategoriesAsync()
        {
            var rawData = await _unitOfWork.Categories.GetAllAsync();
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetCategory>>(rawData);
        }

        public async Task<GetCategory> GetCategoryByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.Categories.GetSingleAsync(c => c.Id == id);
            return rawData is null ? null! : _mapper.Map<GetCategory>(rawData);
        }

        public async Task<GetCategory> GetCategoryByNameAsync(string name)
        {
            var rawData = await _unitOfWork.Categories.GetSingleAsync(c => c.Name == name);
            return rawData is null ? null! : _mapper.Map<GetCategory>(rawData);
        }

        public async Task<ServiceResponse> UpdateCategoryAsync(UpdateCategory category)
        {
            var existingCategory = await _unitOfWork.Categories.GetSingleAsync(c => c.Id == category.Id);
            if (existingCategory == null)
                return new ServiceResponse(false, "Category not found");

            if (category.ParentCategoryId.HasValue)
            {
                var parent = await _unitOfWork.Categories
                    .GetSingleAsync(c => c.Id == category.ParentCategoryId.Value);

                if (parent == null)
                    return new ServiceResponse(false, "Parent category not found");
            }

            _mapper.Map(category, existingCategory);
            await _unitOfWork.Categories.UpdateAsync(existingCategory);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                          ? new ServiceResponse(true, "Category updated successfully!", existingCategory)
                          : new ServiceResponse(false, "Failed to update category.");
        }
        #endregion
    }
}
