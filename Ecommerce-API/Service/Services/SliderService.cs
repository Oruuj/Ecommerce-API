using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.SliderDTOs;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public SliderService(ISliderRepository repository, IMapper mapper, ILogger<CategoryService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateResponse> CreateAsync(SliderCreateDTO entity)
        {
            var directory = Directory.GetCurrentDirectory();
            string imageFolder = Path.Combine(directory, "wwwroot/images");

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            var slider = new Slider
            {
                Title = entity.Title,
                Description = entity.Description,
                ButtonText = entity.ButtonText,
                ButtonUrl = entity.ButtonUrl
            };

            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
            {
                string filename = Guid.NewGuid().ToString() + "---" + entity.ImageFile.FileName;
                string filepath = Path.Combine(imageFolder, filename);

                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await entity.ImageFile.CopyToAsync(stream);
                }

                slider.ImageUrl = Path.Combine("images", filename).Replace("\\", "/");
            }

            await _repository.CreateAsync(slider);
            _logger.LogInformation("Slider created successfully.");

            return new CreateResponse
            {
                StatusCode = 200,
                Message = "Slider created successfully."
            };
        }


        public async Task<CreateResponse> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            _logger.LogInformation($"Slider with ID {id} deleted successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Slider with ID {id} deleted successfully."
            };
        }

        public async Task<IEnumerable<SliderDTO>> GetAllAsync()
        {
            var sliders = await _repository.GetAllAsync();
            _logger.LogInformation("Retrieved all sliders successfully.");
            return _mapper.Map<IEnumerable<SliderDTO>>(sliders);
        }

        public async Task<SliderDTO> GetByIdAsync(int id)
        {
            var slider = await _repository.GetByIdAsync(id);
            _logger.LogInformation($"Retrieved slider with ID {id} successfully.");
            return _mapper.Map<SliderDTO>(slider);
        }

        public async Task<CreateResponse> UpdateAsync(SliderUpdateDTO entity)
        {
            var slider = await _repository.GetByIdAsync(entity.Id);
            if (slider == null)
            {
                _logger.LogWarning($"Slider with ID {entity.Id} not found.");
                return new CreateResponse
                {
                    StatusCode = 404,
                    Message = $"Slider with ID {entity.Id} not found."
                };
            }

            slider.Title = entity.Title ?? slider.Title;
            slider.Description = entity.Description ?? slider.Description;
            slider.ButtonText = entity.ButtonText ?? slider.ButtonText;
            slider.ButtonUrl = entity.ButtonUrl ?? slider.ButtonUrl;

            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
            {
                var directory = Directory.GetCurrentDirectory();
                string imageFolder = Path.Combine(directory, "wwwroot/images");

                if (!Directory.Exists(imageFolder))
                    Directory.CreateDirectory(imageFolder);

                if (!string.IsNullOrEmpty(slider.ImageUrl))
                {
                    string oldImagePath = Path.Combine(directory, "wwwroot", slider.ImageUrl.Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (File.Exists(oldImagePath))
                        File.Delete(oldImagePath);
                }

                string filename = Guid.NewGuid().ToString() + "---" + entity.ImageFile.FileName;
                string filepath = Path.Combine(imageFolder, filename);

                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await entity.ImageFile.CopyToAsync(stream);
                }

                slider.ImageUrl = Path.Combine("images", filename).Replace("\\", "/");
            }

            await _repository.UpdateAsync(slider);
            _logger.LogInformation($"Slider with ID {entity.Id} updated successfully.");

            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Slider with ID {entity.Id} updated successfully."
            };
        }

    }
}
