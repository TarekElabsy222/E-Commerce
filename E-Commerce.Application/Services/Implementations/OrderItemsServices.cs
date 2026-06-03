using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.OrderItemsDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Application.Services.Implementations
{
    public class OrderItemsServices : IOrderItemsServices
    {
        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructor
        public OrderItemsServices(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        public async Task<ServiceResponse> AddItemToOrderAsync(CreateOrderItems item)
        {
            var order = await _unitOfWork.Orders.GetAllAsync(w => w.Id == item.OrderId);

            if (!order.Any()) return new ServiceResponse(false, "The Order you try to add this item on is not exist.");

            var product = await _unitOfWork.Products.GetAllAsync(p => p.Id == item.ProductId);

            if (!product.Any()) return new ServiceResponse(false, "The Product item you try to add is not exist.");

            var mappedData = _mapper.Map<OrderItems>(item);

            await _unitOfWork.OrderItems.AddAsync(mappedData);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Order item added!", mappedData)
                : new ServiceResponse(false, "Failed to add Order item.");
        }

        public async Task<ServiceResponse> DeleteItemFromOrderAsync(Guid id)
        {
            if (id == Guid.Empty) return new ServiceResponse(false, "Invalid Order item id.");

            var item = await _unitOfWork.OrderItems.GetSingleAsync(w => w.Id == id);

            if (item == null) return new ServiceResponse(false, "Order item not found.");

            await _unitOfWork.OrderItems.DeleteAsync(id);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Order item deleted successfully!", item)
                : new ServiceResponse(false, "Failed to delete order item.");
        }

        public async Task<IEnumerable<GetOrderItems>> GetAllOrdersItemsAsync()
        {
            var rawData = await _unitOfWork.OrderItems.GetAllAsync(includeProperties: "Product");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetOrderItems>>(rawData);
        }

        public async Task<GetOrderItems> GetOrderItemByIdAsync(Guid id)
        {
            var rawData = await _unitOfWork.OrderItems.GetSingleAsync(r => r.Id == id, includeProperties: "Product");
            return rawData is null ? null! : _mapper.Map<GetOrderItems>(rawData);
        }

        public async Task<IEnumerable<GetOrderItems>> GetItemsInOrderAsync(Guid orderId)
        {
            var list = await _unitOfWork.Orders.GetSingleAsync(r => r.Id == orderId);
            if (list is null) return [];
            var rawData = await _unitOfWork.OrderItems.GetAllAsync(w => w.OrderId == orderId, includeProperties: "Product");
            if (!rawData.Any()) return [];
            return _mapper.Map<IEnumerable<GetOrderItems>>(rawData);
        }

        public async Task<ServiceResponse> UpdateOrderItemAsync( UpdateOrderItems item)
        {


            var existingItem = await _unitOfWork.OrderItems.GetSingleAsync(w => w.Id == item.Id);

            if (existingItem == null) return new ServiceResponse(false, "Order item not found.");

            var wishlist = await _unitOfWork.Orders.GetSingleAsync(w => w.Id == item.OrderId);

            if (wishlist == null) return new ServiceResponse(false, "order not found.");


            var product = await _unitOfWork.Products.GetSingleAsync(p => p.Id == item.ProductId);

            if (product == null) return new ServiceResponse(false, "Product not found.");

            _mapper.Map(item, existingItem);

            await _unitOfWork.OrderItems.UpdateAsync(existingItem);

            int result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new ServiceResponse(true, "Order item updated successfully!", existingItem)
                : new ServiceResponse(false, "Failed to update order item.");
        }
    }
    #endregion
}
