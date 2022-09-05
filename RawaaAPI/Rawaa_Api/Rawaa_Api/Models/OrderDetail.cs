using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            MealExtras = new HashSet<MealExtra>();
        }

        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public byte? Taste { get; set; }
        public byte? Size { get; set; }
        public byte? Quantity { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? DrinkId { get; set; }

        public virtual Drink? Drink { get; set; }
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;

        public virtual ICollection<MealExtra> MealExtras { get; set; }
    }
}
