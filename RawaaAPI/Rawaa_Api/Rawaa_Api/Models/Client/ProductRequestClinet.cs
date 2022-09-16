namespace Rawaa_Api.Models.Client
{
    public class ProductRequestClinet
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public decimal SmallSizePrice { get; set; }
        public decimal? MediumSizePrice { get; set; }
        public decimal? BigSizePrice { get; set; }
        public decimal? DiscountValue { get; set; }
        public short? Calories { get; set; }
        public byte? HasTaste { get; set; }
        public int? CategoryId { get; set; }
        public string? Title { get; set; }
    }
}
