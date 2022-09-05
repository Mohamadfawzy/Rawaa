using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class Drink
    {
        public Drink()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
