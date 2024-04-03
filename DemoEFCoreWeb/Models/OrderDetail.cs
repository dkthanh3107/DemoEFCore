using System;
using System.Collections.Generic;

namespace ContosoPizza.Models
{
    public partial class OrderDetail
    {
        public object? Product;

        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public int OrderId1 { get; set; }
        public int ProductId1 { get; set; }

        public virtual Order OrderId1Navigation { get; set; } = null!;
        public virtual Product ProductId1Navigation { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
