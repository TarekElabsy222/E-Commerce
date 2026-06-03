using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.OrderItemsDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IOrderItemsServices
    {
        Task<IEnumerable<GetOrderItems>> GetAllOrdersItemsAsync();
        public Task<GetOrderItems> GetOrderItemByIdAsync(Guid id);
        Task<IEnumerable<GetOrderItems>> GetItemsInOrderAsync(Guid orderId);

        Task<ServiceResponse> AddItemToOrderAsync(CreateOrderItems item);
        Task<ServiceResponse> UpdateOrderItemAsync(UpdateOrderItems item);
        Task<ServiceResponse> DeleteItemFromOrderAsync(Guid id);
    }
}
