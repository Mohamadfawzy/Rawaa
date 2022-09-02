using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public decimal? Price { get; set; }
        public int? ProductId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
