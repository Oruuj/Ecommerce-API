using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        private readonly AppDbContext _context;
        public DiscountRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task AddToProduct(int DiscountId, int ProductId)
        {
            await _context.DiscountProducts.AddAsync(new DiscountProduct
            {
                DiscountId = DiscountId,
                ProductId = ProductId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Discount>> GetAllDiscountByProduct(int id) => await _context.Discounts.Where(mbox => mbox.DiscountProducts.Any(mbox => mbox.ProductId == id)).ToListAsync();

        public async Task<IEnumerable<Product>> GetAllProductByDiscount(int id) => await _context.Products.Where(mbox => mbox.DiscountProducts.Any(mbox => mbox.DiscountId == id)).ToListAsync();

        public async Task<Discount> GetByProductIdAsync(int productId) => await _context.Discounts.Include(d => d.DiscountProducts).Where(d => d.DiscountProducts.Any(dp => dp.ProductId == productId)).FirstOrDefaultAsync();
    }
}
