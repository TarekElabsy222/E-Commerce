using E_Commerce.Application.DTOs.Brand;
using E_Commerce.Application.DTOs.CategoryDto;
using E_Commerce.Application.Services.Implementations;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Fields
        private readonly ICategoryServices _categoryServices;
        #endregion

        #region Constructor
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        #endregion

        #region Handle Functions
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var data = await _categoryServices.GetAllCategoriesAsync();
            return data.Any() ? Ok(data) : NotFound(data); 
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var data = await _categoryServices.GetCategoryByIdAsync(id);
            return data !=null ? Ok(data) : NotFound($"Category with id {id} not found");
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var data = await _categoryServices.GetCategoryByNameAsync(name);
            return data != null ? Ok(data) : NotFound($"Category with name: {name} not found");
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory(CreateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _categoryServices.AddCategoryAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(UpdateCategory category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _categoryServices.UpdateCategoryAsync(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var result = await _categoryServices.DeleteCategoryAsync(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion

    }
}
