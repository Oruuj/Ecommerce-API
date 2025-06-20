using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.DiscountDTOs;
using Service.DTOs.ProductDTOs;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class DiscountService : IDiscountService
    {
        private IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;
        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CreateResponse> Create(DiscountCreateDTO entity)
        {
            await _repository.CreateAsync(_mapper.Map<Discount>(entity));
            _logger.LogInformation("Discount created successfully.");
            return new CreateResponse
            {
                StatusCode = 201,
                Message = "Discount created successfully."
            };
        }

        public async Task<CreateResponse> UpdateAsync(DiscountUpdateDTO entity)
        {
            var discount = await _repository.GetByIdAsync(entity.Id);
            if (discount == null)
            {
                _logger.LogWarning($"Discount with ID {entity.Id} not found.");
                return new CreateResponse
                {
                    StatusCode = 404,
                    Message = "Discount not found."
                };
            }

            discount.Name = entity.Name ?? discount.Name;
            discount.DiscountPercentage = entity.DiscountPercentage ?? discount.DiscountPercentage;
            discount.StartDate = entity.StartDate ?? discount.StartDate;
            discount.EndDate = entity.EndDate ?? discount.EndDate;

            await _repository.UpdateAsync(discount);

            _logger.LogInformation("Discount updated successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = "Discount updated successfully."
            };
        }


        public async Task<CreateResponse> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            _logger.LogInformation("Discount deleted successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = "Discount deleted successfully."
            };
        }

        public async Task<IEnumerable<DiscountDTO>> GetAllAsync()
        {
            var Discounts = await _repository.GetAllAsync();
            _logger.LogInformation("Retrieved all discounts successfully.");
            return _mapper.Map<IEnumerable<DiscountDTO>>(Discounts);
        }

        public async Task<DiscountDTO> GetByIdAsync(int id)
        {
            var discount = await _repository.GetByIdAsync(id);
            _logger.LogInformation($"Retrieved discount with ID {id} successfully.");
            return _mapper.Map<DiscountDTO>(discount);
        }
        public async Task<DiscountDTO> GetByProductIdAsync(int Productid)
        {
            var discount = await _repository.GetByProductIdAsync(Productid);
            if (discount == null)
            {
                _logger.LogWarning($"No discount found for product with ID {Productid}.");
                return null;
            }
            _logger.LogInformation($"Retrieved discount for product with ID {Productid} successfully.");
            return _mapper.Map<DiscountDTO>(discount);
        }

        public async Task<IEnumerable<DiscountDTO>> GetAllDiscountByProduct(int id)
        {
            var discounts =  await _repository.GetAllDiscountByProduct(id);
            if (discounts == null)
            {
                _logger.LogWarning($"No discounts found for product with ID {id}.");
                return null;
            }
            _logger.LogInformation($"Retrieved all discounts for product with ID {id} successfully.");
            return _mapper.Map<IEnumerable<DiscountDTO>>(discounts);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductByDiscount(int id)
        {
            var products = await _repository.GetAllProductByDiscount(id);
            if (products == null)
            {
                _logger.LogWarning($"No products found for discount with ID {id}.");
                return null;
            }
            _logger.LogInformation($"Retrieved all products for discount with ID {id} successfully.");
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        public async Task<CreateResponse> AddToProduct(DiscountAddDTO entity)
        {
            await _repository.AddToProduct(entity.DiscountId, entity.ProductId);
            _logger.LogInformation($"Added product with ID {entity.ProductId} to discount with ID {entity.DiscountId} successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Product with ID {entity.ProductId} added to discount with ID {entity.DiscountId} successfully."
            };
        }
    }
}
