
using System.Text.Json.Serialization;
namespace Rawaa_Api.Models.ControlPanel
{
    public class CategoryRq
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TitleAr { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TitleEn { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }
        
    }

    public class CategorySearch
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
    }


}
