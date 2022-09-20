namespace Rawaa_Api.Models
{
    public class OrderStatusRequest
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; } = null!;
        public byte OrderStatus { get; set; } = 0;
        public int? CustomerId { get; set; }
        public int? RestaurantId { get; set; }
        public int? StaffId { get; set; }
    }
}
