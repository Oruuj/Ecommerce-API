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
    public class ProductFeatureRepository : BaseRepository<ProductFeature>, IProductFeatureRepository
    {
        private readonly AppDbContext _context;
        public ProductFeatureRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<ProductFeature>> GetByAllProductId(int ProductId)
        {
            return await _context.ProductFeatures.Where(mbox => mbox.ProductId == ProductId).ToListAsync();
        }

        public async Task<ProductFeature> GetByProductId(int ProductId)
        {
            return await _context.ProductFeatures.FirstOrDefaultAsync(mbox => mbox.ProductId == ProductId);
        }
    }
}
