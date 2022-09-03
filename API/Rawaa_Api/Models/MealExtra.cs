using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class MealExtra
    {
        public MealExtra()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
