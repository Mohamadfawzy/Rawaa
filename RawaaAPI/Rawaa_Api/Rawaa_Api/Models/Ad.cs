using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        [JsonIgnore]
        public byte[]? Image { get; set; }
        public string? ProductId { get; set; }
        public string? CategoryId { get; set; }
    }
}
