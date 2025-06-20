using Microsoft.AspNetCore.Mvc;
using Service.DTOs.ProductSliderDTOs;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.Admin
{
    public class ProductSliderController : AdminController
    {
        private readonly IProductSliderService _service;
        public ProductSliderController(IProductSliderService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductSliderCreateDTO Entity)
        {
            if (Entity == null)
            {
                return BadRequest("Product Slider data is required.");
            }
            return Ok(await _service.Create(Entity));
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductSliderUpdateDTO Entity)
        {
            if (Entity == null)
            {
                return BadRequest("Product Slider data is required.");
            }
            return Ok(await _service.Update(Entity));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Product Slider ID.");
            }
            return Ok(await _service.Delete(id));
        }
    }
}
