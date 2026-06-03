using E_Commerce.Application.DTOs.WishListItemsDto;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistItemsController : ControllerBase
    {
        #region Fields

        private readonly IWishlistItemServices _wishlistItemServices;

        #endregion

        #region Constructor

        public WishlistItemsController(IWishlistItemServices wishlistItemServices)
        {
            _wishlistItemServices = wishlistItemServices;
        }

        #endregion

        #region Handle Functions

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishlistItem(Guid id)
        {
            var data = await _wishlistItemServices.GetWishlistItemByIdAsync(id);

            return data != null ? Ok(data) : NotFound($"Wishlist item with id: {id} not found");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllWishlistItems()
        {
            var data = await _wishlistItemServices.GetAllWishlistItemsAsync();
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpGet("Wishlist/{listId}/Items")]
        public async Task<IActionResult> GetItemsInWishlist(Guid listId)
        {
            var data = await _wishlistItemServices.GetItemsInWishlistAsync(listId);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpPost("AddWishlistItem")]
        public async Task<IActionResult> AddWishlistItem(CreateWishListItems item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _wishlistItemServices.AddWishlistItemAsync(item);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateWishlistItem")]
        public async Task<IActionResult> UpdateWishlistItem(Guid id, UpdateWishListItems item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _wishlistItemServices.UpdateWishlistItemAsync(id, item);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteWishlistItem/{id}")]
        public async Task<IActionResult> DeleteWishlistItem(Guid id)
        {
            var result = await _wishlistItemServices.DeleteWishlistItemAsync(id);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        #endregion
    }
}
