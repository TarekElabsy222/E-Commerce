using E_Commerce.Application.DTOs.CartDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        #region Fields
        private readonly ICartServices _cartServices;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor
        public CartController(ICartServices cartServices, UserManager<ApplicationUser> userManager)
        {
            _cartServices = cartServices;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCarts()
        {
            var data = await _cartServices.GetAllCartsAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCartById(Guid id)
        {
            var data = await _cartServices.GetCartByIdAsync(id);
            return data != null ? Ok(data) : NotFound($"Cart with id {id} not found");
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddCart(CreateCart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return Unauthorized("Please login first");
            var result = await _cartServices.AddCartAsync(currentUser.Id, cart);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart(Guid id, UpdateCart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _cartServices.UpdateCartAsync(id, cart);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var result = await _cartServices.DeleteCartAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion

    }
   
}
