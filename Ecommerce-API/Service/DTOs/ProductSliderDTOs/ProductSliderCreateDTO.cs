using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductSliderDTOs
{
    public class ProductSliderCreateDTO
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public IFormFile ImageFile { get; set; }
        public int? ProductId { get; set; }
        public string ButtonUrl { get; set; }
        public string ButtonText { get; set; }
    }
        
}
