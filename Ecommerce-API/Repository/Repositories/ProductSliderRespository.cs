using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductSliderRespository : BaseRepository<ProductSlider> , IProductSliderRespository
    {
        private readonly AppDbContext _context;
        public ProductSliderRespository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
