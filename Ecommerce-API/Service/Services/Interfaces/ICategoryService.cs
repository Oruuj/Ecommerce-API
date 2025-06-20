using Service.DTOs.CategoryDTOs;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CreateResponse> CreateAsync(CategoryCreateDTO entity);
        Task<CreateResponse> UpdateAsync(CategoryUpdateDTO entity);
        Task<CreateResponse> DeleteAsync(int id);
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
    }
}
