using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.UI
{
    public class ProductController : UIController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _productService.GetAllAsync());
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int Id) => Ok(await _productService.GetByIdAsync(Id));
        [HttpGet]
        public async Task<IActionResult> GetAllWithInclude() => Ok(await _productService.GetAllWithInclude());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdWithIncludeAsync(int id)
        {
            return Ok(await _productService.GetByIdWithIncludeAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetWithPagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string sort = null, [FromQuery] string brands = null, string categories = null)
        {
            return Ok(await _productService.GetWithPagination(page, pageSize, sort, brands, categories));
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string text)
        {
            return Ok(await _productService.Search(text));
        }
    }
}
