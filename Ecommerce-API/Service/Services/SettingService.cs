using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.SettingDTOs;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SettingService> _logger;
        public SettingService(ISettingRepository repository, IMapper mapper, ILogger<SettingService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<CreateResponse> CreateAsync(SettingCreateDTO entity)
        {
            await _repository.CreateAsync(_mapper.Map<Setting>(entity));
            _logger.LogInformation("Setting created successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = "Setting created successfully."
            };
        }

        public async Task<CreateResponse> DeleteAsync(string key)
        {
            await _repository.DeleteByKey(key);
            _logger.LogInformation($"Setting with {key} deleted successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Setting with {key} deleted successfully."
            };
        }

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            var settings = await _repository.GetAllAsync();
            return settings.ToDictionary(s => s.Key, s => s.Value);
        }

        public async Task<List<SettingDTO>> GetAllAsyncForAdmin()
        {
            var settings = await _repository.GetAllAsync();
            return settings.Select(s => new SettingDTO
            {
                Id = s.Id,
                Key = s.Key,
                Value = s.Value
            }).ToList();
        }

        public async Task<SettingDTO> GetByIdAsync(int id) => _mapper.Map<SettingDTO>(await _repository.GetByIdAsync(id));

        public async Task<CreateResponse> UpdateAsync(SettingUpdateDTO entity)
        {
            var setting = await _repository.GetByIdAsync(entity.Id);
            if (setting == null)
                throw new Exception($"Setting with ID {entity.Id} not found.");

            _mapper.Map(entity, setting); 
            await _repository.UpdateAsync(setting);

            _logger.LogInformation($"Setting with ID {entity.Id} updated successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Setting with ID {entity.Id} updated successfully."
            };
        }

    }
}
