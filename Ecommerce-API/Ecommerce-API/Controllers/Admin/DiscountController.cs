using Microsoft.AspNetCore.Mvc;
using Service.DTOs.DiscountDTOs;
using Service.Services.Interfaces;

namespace Ecommerce_API.Controllers.Admin
{
    public class DiscountController : AdminController
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DiscountCreateDTO entity)
        {
            if (entity == null)
            {
                return BadRequest("Discount data is null.");
            }
            var response = await _discountService.Create(entity);
            if (response.StatusCode == 400)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] DiscountUpdateDTO entity)
        {
            if (entity == null)
            {
                return BadRequest("Discount data is null.");
            }
            var response = await _discountService.UpdateAsync(entity);
            if (response.StatusCode == 400)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllDiscountByProduct(int id)
        {
            var discounts = await _discountService.GetAllDiscountByProduct(id);
            return Ok(discounts);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllProductByDiscount(int id)
        {
            var products = await _discountService.GetAllProductByDiscount(id);
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> AddtoProduct([FromBody] DiscountAddDTO entity)
        {
            if (entity.DiscountId <= 0 || entity.ProductId <= 0)
            {
                return BadRequest("Invalid discount or product ID.");
            }
            var responese = await _discountService.AddToProduct(entity);
            return Ok(responese);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) => Ok(await _discountService.DeleteAsync(id));
    }
}
