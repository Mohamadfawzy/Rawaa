using System.Text.Json.Serialization;

namespace Rawaa_Api.Models
{
    public class CategorieTitleTranslation
    {
        public int Id { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; } = null!;
    }
}
