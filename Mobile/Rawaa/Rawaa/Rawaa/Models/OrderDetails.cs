using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.Models
{
    public class OrderDetails
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public byte? Taste { get; set; }
        public byte? Size { get; set; }
        public byte? Quantity { get; set; }
        public double? ProductPrice { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? DrinkId { get; set; } = 1;
        public Product Product { get; set; }
    }
}
