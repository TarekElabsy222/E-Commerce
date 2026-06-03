using E_Commerce.Application.DTOs.WishList;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        #region Fields
        private readonly IWishlistServices _wishListServices;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor
        public WishListController(IWishlistServices wishListServices, UserManager<ApplicationUser> userManager)
        {
            _wishListServices = wishListServices;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        [HttpGet("all")]
        public async Task<IActionResult> GetAllWishLists()
        {
            var data = await _wishListServices.GetAllWishlistAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetWishListById(Guid id)
        {
            var data = await _wishListServices.GetWishlistByIdAsync(id);
            return data != null ? Ok(data) : NotFound($"WishList with id {id} not found");
        }        
        [HttpPost("add")]
        public async Task<IActionResult> AddWishList(CreateWishlist wishList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return Unauthorized("Please login first");
            var result = await _wishListServices.AddWishlistAsync(wishList,currentUser.Id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateWishList(UpdateWishlist wishList)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _wishListServices.UpdateWishlistAsync(wishList);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteWishList(Guid id)
        {
            var result = await _wishListServices.DeleteWishlistAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion
    }
}
