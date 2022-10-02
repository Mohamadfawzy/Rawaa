using System.Text.Json.Serialization;

namespace Rawaa.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public double SmallSizePrice { get; set; }
        public double? MediumSizePrice { get; set; }
        public double? BigSizePrice { get; set; }
        public double? DiscountValue { get; set; }
        public short? Calories { get; set; }
        public bool? HasTaste { get; set; }
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public string ImageUrl => AppSettings.ImageUrl + Image;


    }
}
