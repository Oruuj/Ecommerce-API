using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces;
using Service.DTOs.CategoryDTOs;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ICategoryRepository repository, IMapper mapper, ILogger<CategoryService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateResponse> CreateAsync(CategoryCreateDTO entity)
        {
            var directory = Directory.GetCurrentDirectory();
            string imageFolder = Path.Combine(directory, "wwwroot/images");

            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            var Category = new Category
            {
                Name = entity.Name,
            };

            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
            {
                string filename = Guid.NewGuid().ToString() + "---" + entity.ImageFile.FileName;
                string filepath = Path.Combine(imageFolder, filename);

                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await entity.ImageFile.CopyToAsync(stream);
                }

                Category.ImageUrl = Path.Combine("images", filename).Replace("\\", "/");
            }
            await _repository.CreateAsync(Category);
            _logger.LogInformation("Category created successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = "Category created successfully."
            };
        }

        public async Task<CreateResponse> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            _logger.LogInformation($"Category with ID {id} deleted successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = $"Category with ID {id} deleted successfully."
            };
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            _logger.LogInformation("Retrieved all categories successfully.");
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            _logger.LogInformation($"Retrieved category with ID {id} successfully.");
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CreateResponse> UpdateAsync(CategoryUpdateDTO entity)
        {
            var existingCategory = await _repository.GetByIdAsync(entity.Id);
            if (existingCategory == null)
            {
                _logger.LogWarning($"Category with ID {entity.Id} not found.");
                return new CreateResponse
                {
                    StatusCode = 404,
                    Message = $"Category with ID {entity.Id} not found."
                };
            }

            existingCategory.Name = entity.Name ?? existingCategory.Name;

            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
            {
                var directory = Directory.GetCurrentDirectory();
                string imageFolder = Path.Combine(directory, "wwwroot/images");

                if (!Directory.Exists(imageFolder))
                    Directory.CreateDirectory(imageFolder);

                string filename = Guid.NewGuid().ToString() + "---" + entity.ImageFile.FileName;
                string filepath = Path.Combine(imageFolder, filename);

                using (FileStream stream = new FileStream(filepath, FileMode.Create))
                {
                    await entity.ImageFile.CopyToAsync(stream);
                }

                existingCategory.ImageUrl = Path.Combine("images", filename).Replace("\\", "/");
            }
            else
            {
                existingCategory.ImageUrl = existingCategory.ImageUrl;
            }

            await _repository.UpdateAsync(existingCategory);

            _logger.LogInformation($"Category with ID {entity.Id} updated successfully.");
            return new CreateResponse
            {
                StatusCode = 200,
                Message = "Category updated successfully."
            };
        }

    }
}
