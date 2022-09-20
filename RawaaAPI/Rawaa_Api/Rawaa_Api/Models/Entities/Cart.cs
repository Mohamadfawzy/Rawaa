using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class Cart
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public byte? Quantity { get; set; }
        public byte? Taste { get; set; }
        public byte? Size { get; set; }
        public int? DrinkId { get; set; }
        public DateTime? CreateOn { get; set; }

        public virtual Drink? Drink { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
