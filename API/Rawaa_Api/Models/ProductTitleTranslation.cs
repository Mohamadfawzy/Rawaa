using System.Text.Json.Serialization;

namespace Rawaa_Api.Models
{
    public class ProductTitleTranslation
    {
        public int Id { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public int? ProductId { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; } = null!;
    }
}
