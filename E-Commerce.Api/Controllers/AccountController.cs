using E_Commerce.Application.DTOs.AccountDto;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Fields
        private readonly IAccountServices _accountServices;
        #endregion

        #region Constructor
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        #endregion

        #region Handle Functions
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var account = await _accountServices.RegisterAsync(dto);
                if (account != null)
                {
                    if (account.Success)
                        return Ok(account);
                    return BadRequest(account.Message);
                }
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var account = await _accountServices.LoginAsync(dto);
                if (account != null)
                {
                    if (account.Success)
                        return Ok(account.Model);
                    return BadRequest(account.Model);
                }
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);

        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string email)
        {
            var result = await _accountServices.GetRefreshTokenAsync(email);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAccount([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountServices.DeleteAccountAsync(dto);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordSettingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountServices.ChangePassword(dto);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDto dto, [FromQuery] string currentEmail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountServices.UpdateProfile(dto, currentEmail);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountServices.ResetPassword(dto);

            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }
        #endregion
    }
}
