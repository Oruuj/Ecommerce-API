using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.UI
{
    public class SliderController : UIController
    {
        private readonly ISliderService _service;
        public SliderController(ISliderService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id) => Ok(await _service.GetByIdAsync(Id));
    }
}
