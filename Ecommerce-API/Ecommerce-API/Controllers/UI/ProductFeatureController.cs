using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.UI
{
    public class ProductFeatureController : UIController
    {
        private readonly IProductFeatureService _productFeatureService;
        public ProductFeatureController(IProductFeatureService productFeatureService)
        {
            _productFeatureService = productFeatureService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _productFeatureService.GetAllAsync());
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _productFeatureService.GetByIdAsync(id));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductId(int id)
        {
            var result = await _productFeatureService.GetByProductId(id);
            if (result == null)
            {
                return NotFound($"No product feature found for product ID {id}.");
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByAllProductId(int id)
        {
            var result = await _productFeatureService.GetByAllProductId(id);
            if (result == null || !result.Any())
            {
                return NotFound($"No product features found for product ID {id}.");
            }
            return Ok(result);
        }
    }
}
