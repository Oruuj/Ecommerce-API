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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<Product>> GetAllWithInclude()
        {
            return await _context.Products
                .Include(mbox=>mbox.ProductImages)
                .Include(mbox=>mbox.ProductFeatures)
                .Include(mbox=>mbox.Category)
                .Include(mbox=>mbox.DiscountProducts)
                .ThenInclude(mbox => mbox.Discount)
                .ToListAsync();
        }
        public async Task<Product> GetByIdWithIncludeAsync(int id)
        {
               return await _context.Products
                .Include(mbox=>mbox.ProductImages)
                .Include(mbox=>mbox.ProductFeatures)
                .Include(mbox=>mbox.Category)
                .Include(mbox=>mbox.DiscountProducts)
                .ThenInclude(mbox => mbox.Discount)
                .FirstOrDefaultAsync(mbox=>mbox.Id == id);
        }
        public async Task<List<Product>> Search(string text)
        {
            return await _context.Products
                .Where(p => p.Name.ToLower().Contains(text.Trim().ToLower()))
                .ToListAsync();
        }

    }
}
