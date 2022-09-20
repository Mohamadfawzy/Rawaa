using Rawaa_Api.Models.Entities;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.Client
{
    public class OrderDetailResponse
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; } = null!;
        public byte OrderStatus { get; set; }
        public byte PymentMethod { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal Total { get; set; }
        public DeliveryAddress? Address { get; set; }

        public DateTime? OrderDate { get; set; }


        // product
        public int ProductId { get; set; }
        public string? Title { get; set; }
        
        // delivery address
        public string? ShortName { get; set; } = null!;
        public string? FullAddress { get; set; } = null!;
        
        [JsonIgnore]
        public string? Governorate { get; set; } = null!;
        public string? City { get; set; } = null!;
        public string? Street { get; set; }
        public short? BuildingNumber { get; set; }
        public byte? FloorrUmber { get; set; }
        public byte? ApartmentNumber { get; set; }

        // order details
        public byte? Quantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public byte? Taste { get; set; }
        public byte? Size { get; set; }

    }
}
