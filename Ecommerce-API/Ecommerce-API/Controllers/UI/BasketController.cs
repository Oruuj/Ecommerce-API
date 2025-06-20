using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Service.DTOs.BasketDTOs;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly AppDbContext _context;

    public BasketController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{buyerId}")]
    public async Task<IActionResult> GetBasket(string buyerId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        return basket == null ? NotFound() : Ok(basket);
    }

    [HttpPost("{buyerId}")]
    public async Task<IActionResult> AddItem(string buyerId, [FromBody] BasketItemCreateDto itemDto)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (basket == null)
        {
            basket = new Basket { BuyerId = buyerId };
            _context.Baskets.Add(basket);
        }

        var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == itemDto.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += itemDto.Quantity;
        }
        else
        {
            var item = new BasketItem
            {
                ProductId = itemDto.ProductId,
                ProductName = itemDto.ProductName,
                Price = itemDto.Price,
                Quantity = itemDto.Quantity,
                Basket = basket,
                Image=itemDto.Image
            };
            basket.Items.Add(item);
        }

        await _context.SaveChangesAsync();
        return Ok(basket);
    }


    [HttpDelete("{buyerId}/{productId}")]
    public async Task<IActionResult> RemoveItem(string buyerId, int productId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (basket == null) return NotFound();

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null) return NotFound();

        basket.Items.Remove(item);
        await _context.SaveChangesAsync();

        return Ok(basket);
    }
    [HttpPut("{buyerId}/{productId}")]
    public async Task<IActionResult> UpdateItemQuantity(string buyerId, int productId, [FromBody] int quantity)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .FirstOrDefaultAsync(b => b.BuyerId == buyerId);

        if (basket == null) return NotFound();

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null) return NotFound();

        if (quantity <= 0)
        {
            basket.Items.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        await _context.SaveChangesAsync();
        return Ok(basket);
    }

}
