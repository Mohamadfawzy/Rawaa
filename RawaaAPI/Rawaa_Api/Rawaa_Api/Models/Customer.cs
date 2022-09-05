using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Carts = new HashSet<Cart>();
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            Favorites = new HashSet<Favorite>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
