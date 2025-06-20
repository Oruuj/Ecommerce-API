using Microsoft.AspNetCore.Mvc;
using Service.DTOs.SettingDTOs;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.UI
{
    public class SettingController : UIController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, string>>> GetAll()
        {
            var settings = await _settingService.GetAllAsync();
            return Ok(settings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SettingDTO>> GetById(int id)
        {
            var setting = await _settingService.GetByIdAsync(id);
            if (setting == null)
                return NotFound();

            return Ok(setting);
        }
    }
}
