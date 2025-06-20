using Microsoft.AspNetCore.Mvc;
using Service.DTOs.SettingDTOs;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.Admin
{
    public class SettingController : AdminController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SettingCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _settingService.CreateAsync(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SettingUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(await _settingService.UpdateAsync(dto));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string key) => Ok(await _settingService.DeleteAsync(key));
        [HttpGet]
        public async Task<IActionResult> GetAllAsyncForAdmin() => Ok(await _settingService.GetAllAsync());
    }
}
