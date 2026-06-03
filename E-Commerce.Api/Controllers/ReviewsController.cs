using E_Commerce.Application.DTOs.Review;
using E_Commerce.Application.Services.Implementations;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        #region Fields
        private readonly IReviewServices _reviewServices;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion
        #region Constructor
        public ReviewsController(IReviewServices reviewServices, UserManager<ApplicationUser> userManager)
        {
            _reviewServices = reviewServices;
            _userManager = userManager;
        }
        #endregion
        #region Handle functions
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(Guid id)
        {
            var data = await _reviewServices.GetReviewByIdAsync(id);
            return data != null ? Ok(data) : NotFound($"Review with id: {id} not found");
        }
        [HttpGet("CustomerReviews")]
        public async Task<IActionResult> GetCustomerReviews()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return Unauthorized("Please login first");
            var data = await _reviewServices.GetAllCustomerReviewsAsync(currentUser.Id);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpGet("Product/{productId}/Reviews")]
        public async Task<IActionResult> GetProductReviews(Guid productId)
        {
            var data = await _reviewServices.GetAllProductReviewsAsync(productId);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpGet("CustomerReviewOnProduct/{prodId}")]
        public async Task<IActionResult> GetCustomerReviewOnProduct(Guid prodId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return Unauthorized("Please login first");
            var data = await _reviewServices.GetCustomerReviewsOnProductAsync(currentUser.Id, prodId);
            return data != null ? Ok(data) : NotFound(data);

        }
        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview(CreateReview review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return Unauthorized("Please login first");
            var result = await _reviewServices.AddReviewAsync(review, currentUser.Id);
            return result.Success ? Ok(result) : BadRequest(result);

        }
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> EditReview(UpdateReview review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _reviewServices.UpdateReviewAsync(review);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("DeleteReview/{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var result = await _reviewServices.DeleteReviewAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);

            #endregion
        }
    }
}
