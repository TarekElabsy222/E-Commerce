using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.Review;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IReviewServices
    {
        Task<IEnumerable<GetReview>> GetAllCustomerReviewsAsync(Guid userId);
        Task<IEnumerable<GetReview>> GetAllProductReviewsAsync(Guid productId);
        Task<IEnumerable<GetReview>> GetCustomerReviewsOnProductAsync(Guid userId, Guid productId);
        Task<GetReview> GetReviewByIdAsync(Guid id);
        Task<ServiceResponse> AddReviewAsync(CreateReview review, Guid userId);
        Task<ServiceResponse> UpdateReviewAsync(UpdateReview review);
        Task<ServiceResponse> DeleteReviewAsync(Guid id);
    }
}
