using E_Commerce.Application.DTOs.Brand;
using E_Commerce.Application.Services.Implementations;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        #region Fields
        private readonly IBrandServices _brandServices;
        #endregion

        #region Constructor
        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }
        #endregion

        #region Handle Functions
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBrands()
        {
            var data = await _brandServices.GetAllBrandsAsync();
            return data.Any() ? Ok(data) : NotFound(data); 
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetBrandById(Guid id)
        {
            var data = await _brandServices.GetBrandByIdAsync(id);
            return data !=null ? Ok(data) : NotFound($"Brand with id {id} not found");
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetBrandByName(string name)
        {
            var data = await _brandServices.GetBrandByNameAsync(name);
            return data != null ? Ok(data) : NotFound($"Brand with name: {name} not found");
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddBrand(CreateBrand brand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _brandServices.AddBrandAsync(brand);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBrand(UpdateBrand brand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _brandServices.UpdateBrandAsync(brand);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            var result = await _brandServices.DeleteBrandAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion

    }
}
