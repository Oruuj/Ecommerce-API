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
    public interface IProductFeatureService
    {
        Task<CreateResponse> Create(ProductFeatureCreateDTO entity);
        Task<CreateResponse> UpdateAsync(ProductFeatureUpdateDTO entity);
        Task<CreateResponse> DeleteAsync(int id);
        Task<IEnumerable<ProductFeatureDTO>> GetAllAsync();
        Task<ProductFeatureDTO> GetByIdAsync(int id);
        Task<ProductFeatureDTO> GetByProductId(int id);
        Task<IEnumerable<ProductFeatureDTO>> GetByAllProductId(int id);

    }
}
