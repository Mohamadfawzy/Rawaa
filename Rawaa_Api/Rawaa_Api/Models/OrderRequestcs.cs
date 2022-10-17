namespace Rawaa_Api.Models
{
    public class OrderRequestcs
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; } = null!;
        public byte OrderStatus { get; set; }
        public byte PymentMethod { get; set; }
        public decimal Total { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? DeliveryFee { get; set; }
        public int? RestaurantId { get; set; }
        public int? CustomerId { get; set; }
        public int? DeliveryAddressId { get; set; }
        public int? StaffId { get; set; }

        //
        public int ProductId { get; set; }
        public byte? Taste { get; set; }
        public byte? Size { get; set; }
        public byte? Quantity { get; set; }
        public DateTime? CreateOn { get; set; }
        public int? DrinkId { get; set; }
    }
}
