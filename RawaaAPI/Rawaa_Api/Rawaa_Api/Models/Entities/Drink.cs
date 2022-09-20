using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class Drink
    {
        public Drink()
        {
            DrinksTitleTranslations = new HashSet<DrinksTitleTranslation>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreateOn { get; set; }

        public virtual ICollection<DrinksTitleTranslation> DrinksTitleTranslations { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
