using System.Text.Json.Serialization;

namespace Rawaa.Models
{
    public class AdsM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        
        [JsonIgnore]
        public string ImageUrl =>  Image;
    }
}
