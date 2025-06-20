using Service.DTOs.DiscountDTOs;
using Service.DTOs.ProductDTOs;
using Service.DTOs.ProductFeatureDTOs;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<CreateResponse> Create(DiscountCreateDTO entity);
        Task<CreateResponse> UpdateAsync(DiscountUpdateDTO entity);
        Task<CreateResponse> DeleteAsync(int id);
        Task<IEnumerable<DiscountDTO>> GetAllAsync();
        Task<DiscountDTO> GetByIdAsync(int id);
        Task<IEnumerable<DiscountDTO>> GetAllDiscountByProduct(int id);
        Task<IEnumerable<ProductDTO>> GetAllProductByDiscount(int id);
        Task<CreateResponse> AddToProduct(DiscountAddDTO entity);
        Task<DiscountDTO> GetByProductIdAsync(int Productid);
    }
}
