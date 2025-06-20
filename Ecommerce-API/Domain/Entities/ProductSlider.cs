using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductSlider : BaseEntity
    {
        public string Name  { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public int? ProductId { get; set; }
        public string ButtonUrl { get; set; }
    }
}
