using Rawaa.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rawaa.Models
{
    public  class Order : BaseViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } 
        public byte OrderStatus { get; set; }
        public byte PymentMethod { get; set; }
        public double Total { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? DeliveryFee { get; set; }
        public int? RestaurantId { get; set; }
        public int? CustomerId { get; set; }
        public int? DeliveryAddressId { get; set; }
        public int? StaffId { get; set; }

        DeliveryAddress deliveryAddress;
        public DeliveryAddress DeliveryAddress
        {
            get { return deliveryAddress; }
            set { SetProperty(ref deliveryAddress, value); }
        }
        public List<OrderDetails> OrderDetails { get; set; }

    }
}
