using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductDTOs
{
    public class PaginationDTO
    {
        public List<ProductDTO> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }

    }
}
