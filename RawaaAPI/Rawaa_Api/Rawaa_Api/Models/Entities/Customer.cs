using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.Entities
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
        public string? Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool? EmailVerification { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password { get; set; } = null!;


        [JsonIgnore]
        public virtual ICollection<Cart> Carts { get; set; }
        [JsonIgnore]
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
        [JsonIgnore]
        public virtual ICollection<Favorite> Favorites { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
