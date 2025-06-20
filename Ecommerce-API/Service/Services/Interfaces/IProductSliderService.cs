using Service.DTOs.ProductSliderDTOs;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IProductSliderService
    {
        Task<CreateResponse> Create(ProductSliderCreateDTO entity);
        Task<CreateResponse> Update(ProductSliderUpdateDTO entity);
        Task<CreateResponse> Delete(int Id);
        Task<List<ProductSliderDTO>> GetAll();
        Task<ProductSliderDTO> GetById(int Id);
    }
}
