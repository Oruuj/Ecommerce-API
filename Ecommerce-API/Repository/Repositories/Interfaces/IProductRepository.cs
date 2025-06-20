using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithInclude();
        Task<Product> GetByIdWithIncludeAsync(int id);
        Task<List<Product>> Search(string text);
    }
}
