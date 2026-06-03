using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Review;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;

namespace E_Commerce.Application.Services.Implementations
{
    public class ReviewServices : IReviewServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ReviewServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Handle functions
        public async Task<ServiceResponse> AddReviewAsync(CreateReview review, Guid userId)
        {
            var products = await _unitOfWork.Products.GetAllAsync(p => p.Id == review.ProductId);
            if (!products.Any(p => p.Id == review.ProductId))
            {
                return new ServiceResponse(false, "Product not found.");
            }
           
            var mappedData = _mapper.Map<Review>(review);
            mappedData.CustomerId = userId;
            await _unitOfWork.Reviews.AddAsync(mappedData);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? new ServiceResponse(true, "Review added!", mappedData)
                              : new ServiceResponse(false, "Review failed to added");
        }

        public async Task<ServiceResponse> DeleteReviewAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid review id");

            var review = await _unitOfWork.Reviews
                .GetSingleAsync(p => p.Id == id);
            if (review is null) return new ServiceResponse(false, "Review not found");
            await _unitOfWork.Reviews.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Reviews deleted successfully!")
                : new ServiceResponse(false, "Failed to delete reviews");
        }

        public async Task<IEnumerable<GetReview>> GetAllCustomerReviewsAsync(Guid userId)
        {
            var rawData = await _unitOfWork.Reviews.GetAllAsync(r => r.CustomerId == userId, includeProperties: "Product");
            if (!rawData.Any()) return [];
            return _mapper.Map <IEnumerable<GetReview>> (rawData);
        }

        public async Task<IEnumerable<GetReview>> GetAllProductReviewsAsync(Guid productId)
        {
            var products = await _unitOfWork.Products.GetAllAsync(p => p.Id == productId);
            if (!products.Any(p => p.Id == productId)) return [];
            var rawData = await _unitOfWork.Reviews.GetAllAsync(r => r.ProductId == productId, includeProperties: "Product");
            if (rawData is null) return [];
            return _mapper.Map<List<GetReview>>(rawData);

        }

        public async Task<IEnumerable<GetReview>> GetCustomerReviewsOnProductAsync(Guid userId, Guid productId)
        {
            var products = await _unitOfWork.Products.GetAllAsync(p => p.Id == productId);
            if (!products.Any(p => p.Id == productId)) return [];

            var rawData = await _unitOfWork.Reviews.GetAllAsync(r => r.CustomerId == userId && r.ProductId == productId, includeProperties: "Product");
            if (rawData is null) return [];
            return _mapper.Map<List<GetReview>>(rawData);
        }

        public async Task<GetReview> GetReviewByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.Reviews.GetSingleAsync(r=>r.Id == id,includeProperties: "Product");
            return rawData is null ? null! : _mapper.Map<GetReview>(rawData); 
        }

        public async Task<ServiceResponse> UpdateReviewAsync(UpdateReview review)
        {
            var product = await _unitOfWork.Products.GetSingleAsync(p => p.Id == review.ProductId);

            if (product == null) return new ServiceResponse(false, "Product not found.");

            var existingReview = await _unitOfWork.Reviews.GetSingleAsync(r => r.Id == review.Id);
            if (existingReview == null)
                return new ServiceResponse(false, "Review not found");


            _mapper.Map(review, existingReview);
            await _unitOfWork.Reviews.UpdateAsync(existingReview);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                          ? new ServiceResponse(true, "Review updated successfully!", existingReview)
                          : new ServiceResponse(false, "Failed to update review.");
        }
        #endregion
    }
}
