using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        Task<IEnumerable<Discount>> GetAllDiscountByProduct(int id);
        Task<IEnumerable<Product>> GetAllProductByDiscount(int id);
        Task AddToProduct(int DiscountId, int ProductId);
        Task<Discount> GetByProductIdAsync(int Productid);
    }
}
