using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.AccountDto;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IAccountServices
    {
        public Task<ServiceResponse> LoginAsync(LoginDto login);
        public Task<ServiceResponse> GetRefreshTokenAsync(string email);
        public Task<ServiceResponse> RegisterAsync(RegisterDto register);
        public Task<ServiceResponse> DeleteAccountAsync(LoginDto delete);
        public Task<ServiceResponse> ChangePassword(PasswordSettingDto password);
        public Task<ServiceResponse> UpdateProfile(UserDto user, string currentEmail);
        public Task<ServiceResponse> ResetPassword(ResetPasswordDto reset);
    }
}
