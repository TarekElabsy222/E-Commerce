using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.CategoryDto;
using E_Commerce.Application.DTOs.ProductDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace E_Commerce.Application.Services.Implementations
{
    public class ProductServices: IProductServices
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor        
        public ProductServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Validation Image
        private bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var maxSize = 2 * 1024 * 1024;
            if (file.Length > maxSize)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                return false;

            var allowedMimeTypes = new[]
            {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp"
        };

            if (!allowedMimeTypes.Contains(file.ContentType))
                return false;

            return true;
        }
        #endregion

        #region Handle Functions        
        public async Task<ServiceResponse> AddProductAsync(CreateProduct product)
        {
            // Image Validation
            if (product.ImageFile != null && !IsValidImage(product.ImageFile))
                return new ServiceResponse(false, "Invalid image: allowed (jpg, jpeg, png, gif, webp) and max size 2MB");

            string? imagePath = null;
            if (product.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(stream);
                }

                imagePath = "/images/products/" + fileName;
            }
            var mappedData = _mapper.Map<Product>(product);
            mappedData.Image = imagePath;
            await _unitOfWork.Products.AddAsync(mappedData);
            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Product added successfully!", mappedData)
                : new ServiceResponse(false, "Failed to add product");

        }

        public async Task<ServiceResponse> DeleteProductAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid product id");

            var product = await _unitOfWork.Products
                .GetSingleAsync(p => p.Id == id);

            if (product == null)
                return new ServiceResponse(false, "Product not found");


            if (!string.IsNullOrEmpty(product.Image))
            {
                var imagePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    product.Image.TrimStart('/')
                );

                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            await _unitOfWork.Products.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Product deleted successfully!")
                : new ServiceResponse(false, "Failed to delete product");
        }

        public async Task<IEnumerable<GetProduct>> GetAllProductsAsync()
        {
            var rawData = await _unitOfWork.Products.GetAllAsync(includeProperties: "Brand,Category");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<IEnumerable<GetProduct>> GetProductByBrandIdAsync(Guid brandId)
        {
            var rawData = await _unitOfWork.Products.GetAllAsync(p => p.BrandId == brandId ,includeProperties: "Brand,Category");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<IEnumerable<GetProduct>> GetProductByCategoryIdAsync(Guid categoryId)
        {
            var rawData = await _unitOfWork.Products.GetAllAsync(p => p.CategoryId == categoryId, includeProperties: "Brand,Category");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<GetProduct> GetProductByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.Products.GetSingleAsync(p => p.Id == id, includeProperties: "Brand,Category");
            return rawData is null ? null! : _mapper.Map<GetProduct>(rawData);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByBrandNameAsync(string brandName)
        {
            var rawData = await _unitOfWork.Products.GetAllAsync(p => p.Brand!.Name == brandName, includeProperties: "Brand,Category");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByCategoryNameAsync(string categoryName)
        {
            var rawData = await _unitOfWork.Products.GetAllAsync(p => p.Category!.Name == categoryName, includeProperties: "Brand,Category");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<IEnumerable<GetProduct>> GetProductsByNameAsync(string productName)
        {
            var rawData = await _unitOfWork.Products.GetAllAsync(p => p.Name == productName, includeProperties: "Brand,Category");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetProduct>>(rawData);
        }

        public async Task<ServiceResponse> UpdateProductAsync(UpdateProduct product)
        {

            if (product.Id == Guid.Empty)
                return new ServiceResponse(false, "Invalid product id");

            // Image Validation
            if (product.ImageFile != null && !IsValidImage(product.ImageFile))
                return new ServiceResponse(false, "Invalid image: allowed (jpg, jpeg, png, gif, webp) and max size 2MB");


            var existingProduct = await _unitOfWork.Products
                .GetSingleAsync(p => p.Id == product.Id);

            if (existingProduct == null)
                return new ServiceResponse(false, "Product not found");

            if (product.CategoryId.HasValue)
            {
                var category = await _unitOfWork.Categories
                    .GetSingleAsync(c => c.Id == product.CategoryId);

                if (category == null)
                    return new ServiceResponse(false, "Category not found");
            }

            if (product.BrandId.HasValue)
            {
                var brand = await _unitOfWork.Brands
                    .GetSingleAsync(b => b.Id == product.BrandId);

                if (brand == null)
                    return new ServiceResponse(false, "Brand not found");
            }

            if (product.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(existingProduct.Image))
                {
                    var oldImagePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        existingProduct.Image.TrimStart('/')
                    );

                    if (File.Exists(oldImagePath))
                        File.Delete(oldImagePath);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(stream);
                }

                existingProduct.Image = "/images/products/" + fileName;
            }

            _mapper.Map(product, existingProduct);

            await _unitOfWork.Products.UpdateAsync(existingProduct);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Product updated successfully!", existingProduct)
                : new ServiceResponse(false, "Failed to update product");
        }
        #endregion
    }
}
