using Domain.Entities;
using Service.DTOs.ProductDTOs;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<CreateResponse> CreateAsync(ProductCreateDTO entity);
        Task<CreateResponse> UpdateAsync(ProductUpdateDTO entity);
        Task<CreateResponse> DeleteAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> GetByIdWithIncludeAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllWithInclude();
        Task<PaginationDTO> GetWithPagination(int page = 1, int pageSize = 6, string sort = null, string brands = null,string categories= null);
        Task<List<ProductDTO>> Search(string text);
    }
}
