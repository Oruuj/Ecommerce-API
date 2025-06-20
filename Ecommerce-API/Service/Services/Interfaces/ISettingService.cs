using Service.DTOs.SettingDTOs;
using Service.DTOs.SliderDTOs;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ISettingService
    {
        Task<CreateResponse> CreateAsync(SettingCreateDTO entity);
        Task<CreateResponse> UpdateAsync(SettingUpdateDTO entity);
        Task<CreateResponse> DeleteAsync(string key);
        Task<Dictionary<string, string>> GetAllAsync();
        Task<SettingDTO> GetByIdAsync(int id);
        Task<List<SettingDTO>> GetAllAsyncForAdmin();
    }
}
