using Rawaa;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public string ImageUrl => AppSettings.ApiUrl + Image;
    }
}
