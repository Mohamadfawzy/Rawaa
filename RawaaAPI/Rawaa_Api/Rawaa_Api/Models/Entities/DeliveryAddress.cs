using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.Entities
{
    public partial class DeliveryAddress
    {
        public DeliveryAddress()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Governorate { get; set; } = null!;
        public string? City { get; set; } = null!;
        public string? Street { get; set; }
        public short? BuildingNumber { get; set; }
        public byte? FloorrUmber { get; set; }
        public byte? ApartmentNumber { get; set; }
        public string? Notes { get; set; }
        public string? ShortName { get; set; } = null!;
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer? Customer { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
