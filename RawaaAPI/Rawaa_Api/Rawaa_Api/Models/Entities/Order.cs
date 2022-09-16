using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? OrderNumber { get; set; } = null!;
        public decimal Total { get; set; }
        public decimal? DeliveryFee { get; set; }
        public byte OrderStatus { get; set; }
        public byte PymentMethod { get; set; }

        public DateTime? OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public int? DeliveryAddressId { get; set; }
        public int? RestaurantId { get; set; }
        public int? StaffId { get; set; }
        [JsonIgnore]
        public virtual Customer? Customer { get; set; }
        [JsonIgnore]
        public virtual DeliveryAddress? DeliveryAddress { get; set; }
        [JsonIgnore]
        public virtual Restaurant? Restaurant { get; set; }
        [JsonIgnore]
        public virtual Staff? Staff { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
