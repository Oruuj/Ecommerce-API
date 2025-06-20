using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.UI
{
    public class DiscountController : UIController
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _discountService.GetAllAsync();
            if (discounts == null || !discounts.Any())
            {
                return NotFound("No discounts found.");
            }
            return Ok(discounts);
        }
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid discount ID.");
            }
            var discount = await _discountService.GetByIdAsync(id);
            if (discount == null)
            {
                return NotFound($"Discount with ID {id} not found.");
            }
            return Ok(discount);
        }
    }
}
