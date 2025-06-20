using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.DTOs.ProductFeatureDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductDTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public List<ProductFeatureCreateDTO>? ProductFeatures { get; set; }
        public List<DiscountProduct>? DiscountProducts { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
