using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductFeatureDTOs
{
    public class ProductFeatureCreateDTO
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }
    }
}
