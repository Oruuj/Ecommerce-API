using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DiscountProduct : BaseEntity
    {
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
