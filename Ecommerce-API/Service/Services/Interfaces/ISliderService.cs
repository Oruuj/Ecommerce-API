using Service.DTOs.CategoryDTOs;
using Service.DTOs.SliderDTOs;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISliderService
    {
        Task<CreateResponse> CreateAsync(SliderCreateDTO entity);
        Task<CreateResponse> UpdateAsync(SliderUpdateDTO entity);
        Task<CreateResponse> DeleteAsync(int id);
        Task<IEnumerable<SliderDTO>> GetAllAsync();
        Task<SliderDTO> GetByIdAsync(int id);
    }
}
