using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.Entities
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
        public decimal? ProductPrice { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? DrinkId { get; set; }

        [JsonIgnore]
        public virtual Drink? Drink { get; set; }
        [JsonIgnore]
        public virtual Order? Order { get; set; } = null!;
        [JsonIgnore]
        public virtual Product? Product { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<MealExtra>? MealExtras { get; set; }
    }
}
