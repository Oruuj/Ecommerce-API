using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Service.DTOs.ProductDTOs
{
    public class ProductUpdateDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Brand { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
