using E_Commerce.Application.DTOs.PayMentDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        #region Fields
        private readonly IPaymentServices _paymentServices;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion
        #region Constructor
        public PaymentController(IPaymentServices paymentServices, UserManager<ApplicationUser> userManager)
        {
            _paymentServices = paymentServices;
            _userManager = userManager;
        }
        #endregion
        #region Handle functions
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            var data = _paymentServices.GetPayment(id);
            return data != null ? Ok(data) : NotFound($"Payment with id: {id} not found");
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllPayment()
        {          
            var data = await _paymentServices.GetAllPayments();
            return data != null ? Ok(data) : NotFound(data);
        }

       
        [HttpPost("Checkout")]
        public async Task<IActionResult> CreateCheckOutSession(CreatePayment payment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is null) return Unauthorized("Please login first");
            var result = await _paymentServices.CreateCheckoutSession(payment, currentUser);
            return result.Success ? Ok(result) : BadRequest(result);

        }        
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            var result = await _paymentServices.DeletePayment(id);
            return result.Success ? Ok(result) : BadRequest(result);

            #endregion
        }
    }
}
