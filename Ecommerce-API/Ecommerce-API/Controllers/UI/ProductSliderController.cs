using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.UI
{
    public class ProductSliderController : UIController
    {
        private readonly IProductSliderService _service;
        public ProductSliderController(IProductSliderService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productSliders = await _service.GetAll();
            return Ok(productSliders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var productSlider = await _service.GetById(id);
            if (productSlider == null)
            {
                return NotFound();
            }
            return Ok(productSlider);
        }
    }
}
