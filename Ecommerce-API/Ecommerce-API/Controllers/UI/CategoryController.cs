using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.UI
{
    public class CategoryController : UIController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _categoryService.GetAllAsync());
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int Id) => Ok(await _categoryService.GetByIdAsync(Id));
    }
}
