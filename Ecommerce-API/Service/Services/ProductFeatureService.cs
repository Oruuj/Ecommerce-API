using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.DTOs.ProductFeatureDTOs;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductFeatureService : IProductFeatureService
    {
        private readonly IProductFeatureRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductFeature> _logger;
        private readonly IProductRepository _productRepository;
        public ProductFeatureService(IProductFeatureRepository productRepository, IMapper mapper, ILogger<ProductFeature> logger)
        {
            _repository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<CreateResponse> Create(ProductFeatureCreateDTO entity)
        {
            await _repository.CreateAsync(_mapper.Map<ProductFeature>(entity));
            _logger.LogInformation("Product feature created successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = "Product feature created successfully."
            };
        }

        public async Task<CreateResponse> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            _logger.LogInformation($"Product feature with ID {id} deleted successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Product feature with ID {id} deleted successfully."
            };
        }

        public async Task<IEnumerable<ProductFeatureDTO>> GetAllAsync()
        {
            var productFeatures = await _repository.GetAllAsync();
            _logger.LogInformation("Retrieved all product features successfully.");
            return _mapper.Map<IEnumerable<ProductFeatureDTO>>(productFeatures);
        }

        public async Task<IEnumerable<ProductFeatureDTO>> GetByAllProductId(int id)
        {
            var productFeatures = await _repository.GetByAllProductId(id);
            if (productFeatures == null || !productFeatures.Any())
            {
                _logger.LogWarning($"No product features found for product ID {id}.");
                return null;
            }
            _logger.LogInformation($"Retrieved product features for product ID {id} successfully.");
            return _mapper.Map<IEnumerable<ProductFeatureDTO>>(productFeatures);
        }
        public async Task<ProductFeatureDTO> GetByIdAsync(int id)
        {
            var productFeature = await _repository.GetByIdAsync(id);
            if (productFeature == null)
            {
                _logger.LogWarning($"Product feature with ID {id} not found.");
                return null;
            }
            _logger.LogInformation($"Retrieved product feature with ID {id} successfully.");
            return _mapper.Map<ProductFeatureDTO>(productFeature);
        }

        public async Task<ProductFeatureDTO> GetByProductId(int id)
        {
            var product = await _repository.GetByProductId(id);
            if (product == null)
            {
                _logger.LogWarning($"Product feature for product ID {id} not found.");
                return null;
            }
            _logger.LogInformation($"Retrieved product feature for product ID {id} successfully.");
            return _mapper.Map<ProductFeatureDTO>(product);
        }

        public async Task<CreateResponse> UpdateAsync(ProductFeatureUpdateDTO entity)
        {
            var productFeature = await _repository.GetByIdAsync(entity.Id);
            if (productFeature == null)
            {
                _logger.LogWarning($"Product feature with ID {entity.Id} not found");
                return new CreateResponse
                {
                    StatusCode = 404,
                    Message = $"Product feature with ID {entity.Id} not found"
                };
            }

            productFeature.Name = entity.Name ?? productFeature.Name;
            productFeature.Value = entity.Value ?? productFeature.Value;
            productFeature.ProductId = entity.ProductId.Value;
            await _repository.UpdateAsync(productFeature);
            _logger.LogInformation($"Product feature with ID {entity.Id} updated successfully.");

            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Product feature with ID {entity.Id} updated successfully."
            };
        }

    }
}
