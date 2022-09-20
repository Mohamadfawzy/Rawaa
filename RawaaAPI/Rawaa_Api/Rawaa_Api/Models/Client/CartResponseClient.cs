namespace Rawaa_Api.Models.Client
{
    public class CartResponseClient
    {
        public int CustomerId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public byte? Quantity { get; set; } = 1;
        public byte? Taste { get; set; } = 1;
        public byte? Size { get; set; } = 1;
        public int DrinkId { get; set; } = 0;
        public DateTime? CreateOn { get; set; }
        public ProductRequestClinet? Product { get; set; } = null!;
    }
}
