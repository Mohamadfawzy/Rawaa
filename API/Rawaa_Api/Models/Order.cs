using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public byte OrderStatus { get; set; }
        public byte PymentMethod { get; set; }
        public decimal Total { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? DeliveryFee { get; set; }
        public int? RestaurantId { get; set; }
        public int CustomersId { get; set; }
        public int DeliveryAddressId { get; set; }

        public virtual Customer Customers { get; set; } = null!;
        public virtual DeliveryAddress DeliveryAddress { get; set; } = null!;
        public virtual Restaurant? Restaurant { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
