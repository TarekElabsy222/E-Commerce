using E_Commerce.Application.DTOs.OrderItemsDto;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        #region Fields

        private readonly IOrderItemsServices _orderItemsServices;

        #endregion

        #region Constructor

        public OrderItemsController(IOrderItemsServices orderItemsServices)
        {
            _orderItemsServices = orderItemsServices;
        }

        #endregion

        #region Handle Functions

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItems(Guid id)
        {
            var data = await _orderItemsServices.GetOrderItemByIdAsync(id);

            return data != null ? Ok(data) : NotFound($"Order item with id: {id} not found");
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var data = await _orderItemsServices.GetAllOrdersItemsAsync();
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpGet("Order/{listId}/Items")]
        public async Task<IActionResult> GetItemsInOrder(Guid listId)
        {
            var data = await _orderItemsServices.GetItemsInOrderAsync(listId);
            return data != null ? Ok(data) : NotFound(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrderItems(CreateOrderItems item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderItemsServices.AddItemToOrderAsync(item);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrderItems(UpdateOrderItems item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderItemsServices.UpdateOrderItemAsync(item);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteOrderItems(Guid id)
        {
            var result = await _orderItemsServices.DeleteItemFromOrderAsync(id);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        #endregion
    }
}
