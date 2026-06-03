using AutoMapper;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.DTOs.AccountDto;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Cryptography;

namespace E_Commerce.Application.Services.Implementations
{
    public class AccountServices : IAccountServices
    {

        #region Field
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public AccountServices(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Handle Functions        
        public async Task<ServiceResponse> ChangePassword(PasswordSettingDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.CurrentPassword))
                return new ServiceResponse(Success: false, Message: "Email or current password is incorrect!");

            var result = await _userManager.ChangePasswordAsync(user,dto.CurrentPassword, dto.NewPassword );

            if (!result.Succeeded) return new ServiceResponse( Success: false,Message: "Failed to change password.",Model: result.Errors.Select(e => e.Description));

            return new ServiceResponse( Success: true, Message: "Password changed successfully.");
        }

        public async Task<ServiceResponse> DeleteAccountAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return new ServiceResponse( Success: false, Message: "Email or password is incorrect!");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return new ServiceResponse( Success: false, Message: "Failed to delete account.", Model: result.Errors.Select(e => e.Description) );

            return new ServiceResponse( Success: true, Message: "Account deleted successfully.");
        }

        public async Task<ServiceResponse> GetRefreshTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null) return new ServiceResponse(Success: false, Message: "Invalid email!");

            var activeToken = user.RefreshTokens?.FirstOrDefault(t => t.IsActive);

            if (activeToken is not null) return new ServiceResponse(Success: false, Message: "There is already an active refresh token.");
            

            var refreshToken = CreateRefreshToken();

            var jwtToken = await CreateToken(user);

            user.RefreshTokens!.Add(refreshToken);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return new ServiceResponse(Success: false, Message: "Failed to update user refresh token.");

            var authDto = new AuthDto
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresOn,
                Email = user.Email!,
                UserName = user.UserName!,
                Message = "Refresh token created successfully."
            };

            return new ServiceResponse(Success: false, Message: "Succes.",Model: authDto);
            ;
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new ServiceResponse
                {                    
                    Success = false,
                    Model = new AuthDto
                    {
                        Token = string.Empty,
                        Email = string.Empty,
                        Role = string.Empty,
                        UserName = string.Empty,
                        IsAuthenticated = false,
                        Message = "Invalid Email or Password!",
                    }
                };
            }

            var JwtToken = await CreateToken(user);
            var refreshToken = "";
            DateTime refreshTokenExpiration;

            if (user.RefreshTokens!.Any(t => t.IsActive))
            {
                var activeToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                refreshToken = activeToken.Token;
                refreshTokenExpiration = activeToken.ExpiresOn;
            }
            else
            {
                var RefreshToken = CreateRefreshToken();
                refreshToken = RefreshToken.Token;
                refreshTokenExpiration = RefreshToken.ExpiresOn;
                user.RefreshTokens.Add(RefreshToken);
                await _userManager.UpdateAsync(user);
            }

            return new ServiceResponse
            {                
                Success = true,
                Model = new AuthDto
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                    UserName = user.UserName,
                    Email = user.Email,
                    Message = "Signed in Successfully",
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = refreshTokenExpiration
                }

            };
        }


        public async Task<ServiceResponse> RegisterAsync(RegisterDto register)
        {
            if (await _userManager.FindByNameAsync(register.UserName) != null)
            {
                return new ServiceResponse(false, "This user already exists!");
            }
            var user = _mapper.Map<ApplicationUser>(register);
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, \n";
                }
                return new ServiceResponse(false, errors);
            }

            await _userManager.AddToRoleAsync(user, "Customer");
            var JwtToken = await CreateToken(user);
            return new ServiceResponse
            {
                Message = "Account created Successfully.",
                Success = true,
                Model = new
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                    UserName = user.UserName,
                }
            };
        }

        public async Task<ServiceResponse> ResetPassword(ResetPasswordDto reset)
        {
            var user = await _userManager.FindByEmailAsync(reset.Email);

            if (user is null) return new ServiceResponse(Success: false, Message: "Invalid email!");


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync( user,token, reset.NewPassword);

            if (!result.Succeeded) return new ServiceResponse(Success: false, Message: "Failed to reset password.",Model: result.Errors.Select(e => e.Description));

            return new ServiceResponse(Success: true, Message: "Password reset successfully.");            
        }

        public async Task<ServiceResponse> UpdateProfile(UserDto dto, string currentEmail)
        {
            var user = await _userManager.FindByEmailAsync(currentEmail);

            if (user is null) return new ServiceResponse(Success: false,Message: "User not found.");

            try
            {
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.PhoneNumber = dto.PhoneNumber;
                user.Email = dto.Email;
                user.UserName = dto.UserName;
                user.address = dto.Address;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded) return new ServiceResponse( Success: false,Message: "Failed to update profile.", Model: result.Errors.Select(e => e.Description));

                return new ServiceResponse(Success: true,Message: "Profile updated successfully.", Model: dto );
            }
            catch
            {
                return new ServiceResponse( Success: false, Message: "An error occurred while updating profile.");
            }
        }


        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var JwtToken = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                signingCredentials: signing,
                expires: DateTime.Now.AddHours(6)
                );
            return JwtToken;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddHours(6),
                CreatedOn = DateTime.UtcNow
            };
        }
       
        #endregion
    }
}
