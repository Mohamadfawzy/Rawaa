
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.Entities
{
    public partial class Cart
    {
        public int CustomerId { get; set; } =0;
        public int ProductId { get; set; } = 0;
        public byte? Quantity { get; set; } = 1;
        public byte? Taste { get; set; } = 1;
        public byte? Size { get; set; } = 1;
        public int DrinkId { get; set; } = 0;
        public DateTime? CreateOn { get; set; }

        [JsonIgnore]
        public virtual Drink? Drink { get; set; }
        [JsonIgnore]
        public virtual Customer? Customer { get; set; } = null!;
        [JsonIgnore]
        public virtual Product? Product { get; set; } = null!;


    }
}
