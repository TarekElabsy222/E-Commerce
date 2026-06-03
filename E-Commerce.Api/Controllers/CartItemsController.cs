using E_Commerce.Application.DTOs.CartItemsDto;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        #region Fields

        private readonly ICartItemsServices _cartItemServices;

        #endregion

        #region Constructor

        public CartItemsController(ICartItemsServices cartItemServices)
        {
            _cartItemServices = cartItemServices;
        }

        #endregion

        #region Handle Functions

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCartItems(Guid id)
        {
            var data = await _cartItemServices.GetCartItemByIdAsync(id);

            return data != null ? Ok(data) : NotFound($"Cart item with id: {id} not found");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCartItems()
        {
            var data = await _cartItemServices.GetAllCartsItemsAsync();
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpGet("Cart/{listId}/Items")]
        public async Task<IActionResult> GetItemsInCart(Guid listId)
        {
            var data = await _cartItemServices.GetItemsInCartAsync(listId);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCartItems(CreateCartItems item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartItemServices.AddItemToCartAsync(item);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItems(Guid id, UpdateCartItems item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartItemServices.UpdateCartItemAsync(item);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete/id/{id}")]
        public async Task<IActionResult> DeleteCartItems(Guid id)
        {
            var result = await _cartItemServices.DeleteItemFromCartAsync(id);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        #endregion
    }
}
