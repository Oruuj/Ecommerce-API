using Microsoft.AspNetCore.Mvc;
using Service.DTOs.SliderDTOs;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.Admin
{
    public class SliderController : AdminController
    {
        private readonly ISliderService _service;
        public SliderController(ISliderService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDTO request)
        {
            var result = await _service.CreateAsync(request);
            if (result.StatusCode==200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] SliderUpdateDTO request)
        {
            var result = await _service.UpdateAsync(request);
            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
