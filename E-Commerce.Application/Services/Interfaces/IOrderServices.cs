using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.OrderDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<GetOrder> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<GetOrder>> GetCustomerOrdersAsync();

        Task<ServiceResponse> AddOrderAsync(CreateOrder cart);
        Task<ServiceResponse> UpdateOrderAsync(UpdateOrder cart);
        Task<ServiceResponse> DeleteOrderAsync(Guid id);
    }
}
