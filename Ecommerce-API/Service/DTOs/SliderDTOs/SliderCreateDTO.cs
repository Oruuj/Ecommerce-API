using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.SliderDTOs
{
    public class SliderCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
    }
}
