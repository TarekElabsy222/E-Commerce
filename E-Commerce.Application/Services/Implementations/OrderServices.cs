using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.OrderDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Services.Implementations
{
    public class OrderServices : IOrderServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructor
        public OrderServices(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        #endregion

        #region Handle functions
        public async Task<ServiceResponse> AddOrderAsync(CreateOrder order)
        {
            var mappedData = _mapper.Map<Order>(order);
            var user = await GetCurrentUser();
            if (user != null)
                mappedData.CustomerId = user.Id;

            await _unitOfWork.Orders.AddAsync(mappedData);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? new ServiceResponse(true, "Order added!", mappedData)
                              : new ServiceResponse(false, "Order failed to added");
        }

        public async Task<ServiceResponse> DeleteOrderAsync(Guid id)
        {
            if (id == Guid.Empty)
                return new ServiceResponse(false, "Invalid cart id");

            var order = await _unitOfWork.Orders.GetSingleAsync(p => p.Id == id);
            if (order is null) return new ServiceResponse(false, "Order not found");
            await _unitOfWork.Orders.DeleteAsync(id);
            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Order deleted successfully!")
                : new ServiceResponse(false, "Failed to delete carts");
        }

        public async Task<IEnumerable<GetOrder>> GetCustomerOrdersAsync()
        {
            var rawData = await _unitOfWork.Orders.GetAllAsync();
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetOrder>>(rawData);
        }

        public async Task<GetOrder> GetOrderByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.Orders.GetSingleAsync(c => c.Id == id);
            return rawData is null ? null! : _mapper.Map<GetOrder>(rawData);
        }

        public async Task<ServiceResponse> UpdateOrderAsync(UpdateOrder order)
        {
            var existingOrder = await _unitOfWork.Orders.GetSingleAsync(r => r.Id == order.Id);
            if (existingOrder == null)
                return new ServiceResponse(false, "Order not found");


            _mapper.Map(order, existingOrder);
            await _unitOfWork.Orders.UpdateAsync(existingOrder);
            int result = await _unitOfWork.SaveChangesAsync();
            return result > 0
                          ? new ServiceResponse(true, "Order updated successfully!", existingOrder)
                          : new ServiceResponse(false, "Failed to update cart.");
        }


        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userClaim = _httpContextAccessor.HttpContext!.User;
            return await _userManager.GetUserAsync(userClaim);
        }
        #endregion
    }
}
