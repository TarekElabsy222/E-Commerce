using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.PayMentDto;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IPaymentServices
    {
        Task<ServiceResponse> CreateCheckoutSession(CreatePayment dto, ApplicationUser user);

        public Task<IEnumerable<GetPayment>> GetAllPayments();
        public Task<GetPayment> GetPayment(Guid id);

        public Task<ServiceResponse> DeletePayment(Guid id);
    }
}
