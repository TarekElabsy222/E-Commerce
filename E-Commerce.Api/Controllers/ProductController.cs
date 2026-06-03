using E_Commerce.Application.DTOs.ProductDto;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        private readonly IProductServices _productServices;
        #endregion

        #region Constructor
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        #endregion

        #region Handle Functions
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var data = await _productServices.GetAllProductsAsync();
            return data.Any() ? Ok(data) : NotFound(data);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var data = await _productServices.GetProductByIdAsync(id);
            return data != null ? Ok(data) : NotFound($"Product with id {id} not found");
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var data = await _productServices.GetProductsByNameAsync(name);
            return data != null ? Ok(data) : NotFound($"Product with name {name} not found");
        }
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetProductByCategoryId(Guid id)
        {
            var data = await _productServices.GetProductByCategoryIdAsync(id);
            return data != null ? Ok(data) : NotFound($"Product with category id {id} not found");
        }

        [HttpGet("product/name/{name}")]
        public async Task<IActionResult> GetProductByCategoryName(string name)
        {
            var data = await _productServices.GetProductsByCategoryNameAsync(name);
            return data != null ? Ok(data) : NotFound($"Product with category name {name} not found");
        }

        [HttpGet("brand/{id}")]
        public async Task<IActionResult> GetProductByBrandId(Guid id)
        {
            var data = await _productServices.GetProductByBrandIdAsync(id);
            return data != null ? Ok(data) : NotFound($"Product with brand id {id} not found");
        }

        [HttpGet("brand/name/{name}")]
        public async Task<IActionResult> GetProductByBrandName(string name)
        {
            var data = await _productServices.GetProductsByBrandNameAsync(name);
            return data != null ? Ok(data) : NotFound($"Product with brand name {name} not found");
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct( [FromForm] CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productServices.AddProductAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _productServices.UpdateProductAsync(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _productServices.DeleteProductAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion
    }
}
