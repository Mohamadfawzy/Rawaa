using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class DeliveryAddress
    {
        public DeliveryAddress()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string GovernorateAr { get; set; } = null!;
        public string CityAr { get; set; } = null!;
        public string? StreetAr { get; set; }
        public short? BuildingNumber { get; set; }
        public byte? FloorrUmber { get; set; }
        public byte? ApartmentNumber { get; set; }
        public string? GovernorateEn { get; set; }
        public string? CityEn { get; set; }
        public string? StreetEn { get; set; }
        public string? Notes { get; set; }
        public string ShortName { get; set; } = null!;
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
