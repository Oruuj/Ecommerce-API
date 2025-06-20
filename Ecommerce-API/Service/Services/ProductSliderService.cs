using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.ProductSliderDTOs;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductSliderService : IProductSliderService
    {
        private readonly IProductSliderRespository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductSliderService> _logger;
        public ProductSliderService(IProductSliderRespository repository, IMapper mapper, ILogger<ProductSliderService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateResponse> Create(ProductSliderCreateDTO entity)
        {
            var directory = Directory.GetCurrentDirectory();
            string imageFolder = Path.Combine(directory, "wwwroot/images");

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            var ProductSlider = new ProductSlider
            {
                Name = entity.Name,
                Desc = entity.Desc,
                ProductId = entity.ProductId,
                ButtonUrl = entity.ButtonUrl,
            };

            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
            {
                string filename = Guid.NewGuid().ToString() + "---" + entity.ImageFile.FileName;
                string filepath = Path.Combine(imageFolder, filename);

                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await entity.ImageFile.CopyToAsync(stream);
                }

                ProductSlider.ImageUrl = Path.Combine("images", filename).Replace("\\", "/");
            }
            await _repository.CreateAsync(ProductSlider);
            _logger.LogInformation("ProductSlider created successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = "ProductSlider created successfully."
            };
        }

        public async Task<CreateResponse> Delete(int Id)
        {
            var slider = await _repository.GetByIdAsync(Id);
            if (slider == null)
            {
                _logger.LogWarning($"Product slider with ID {Id} not found for deletion.");
                return new CreateResponse
                {
                    StatusCode = 404,
                    Message = $"Product slider with ID {Id} not found."
                };
            }
            await _repository.DeleteAsync(Id);
            _logger.LogInformation($"Product slider with ID {Id} deleted successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Product slider with ID {Id} deleted successfully."
            };
        }

        public async Task<List<ProductSliderDTO>> GetAll()
        {
            var sliders = await _repository.GetAllAsync();
            if (sliders == null || !sliders.Any())
            {
                _logger.LogWarning("No product sliders found.");
                return new List<ProductSliderDTO>();
            }
            _logger.LogInformation("Product sliders retrieved successfully.");
            return _mapper.Map<List<ProductSliderDTO>>(sliders);
        }

        public async Task<ProductSliderDTO> GetById(int Id)
        {
            var slider = await _repository.GetByIdAsync(Id);
            if (slider == null)
            {
                _logger.LogWarning($"Product slider with ID {Id} not found.");
                return null;
            }
            _logger.LogInformation($"Product slider with ID {Id} retrieved successfully.");
            return _mapper.Map<ProductSliderDTO>(slider);
        }

        public async Task<CreateResponse> Update(ProductSliderUpdateDTO entity)
        {
            var slider = await _repository.GetByIdAsync(entity.Id);
            if (slider == null)
            {
                _logger.LogWarning($"Product slider with ID {entity.Id} not found for update.");
            }
            var directory = Directory.GetCurrentDirectory();
            string imageFolder = Path.Combine(directory, "wwwroot/images");

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            slider.Name = entity.Name ?? slider.Name;
            slider.Desc = entity.Desc ?? slider.Desc;
            slider.ProductId = entity.ProductId != 0 ? entity.ProductId : slider.ProductId;
            slider.ButtonUrl = entity.ButtonUrl ?? slider.ButtonUrl;

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
            else
            {
                slider.ImageUrl = slider.ImageUrl;
            }
                await _repository.UpdateAsync(slider);
            _logger.LogInformation($"Product slider with ID {entity.Id} updated successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Product slider with ID {entity.Id} updated successfully."
            };
        }
    }
}
