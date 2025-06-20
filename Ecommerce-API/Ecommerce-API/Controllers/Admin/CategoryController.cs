using Microsoft.AspNetCore.Mvc;
using Service.DTOs.CategoryDTOs;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.Admin
{
    public class CategoryController : AdminController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int Id) => Ok(await _categoryService.DeleteAsync(Id));
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDTO entity) => Ok(await _categoryService.CreateAsync(entity));
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDTO entity) => Ok(await _categoryService.UpdateAsync(entity));
    }
}
