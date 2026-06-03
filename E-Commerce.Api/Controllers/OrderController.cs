using E_Commerce.Application.DTOs.OrderDto;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Fields
        private readonly IOrderServices _orderServices;
        #endregion

        #region Constructor
        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        #endregion

        #region Handle Functions
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var data = await _orderServices.GetCustomerOrdersAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var data = await _orderServices.GetOrderByIdAsync(id);
            return data != null ? Ok(data) : NotFound($"Order with id {id} not found");
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrder(CreateOrder order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _orderServices.AddOrderAsync(order);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder(UpdateOrder order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _orderServices.UpdateOrderAsync(order);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderServices.DeleteOrderAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion

    }
}

