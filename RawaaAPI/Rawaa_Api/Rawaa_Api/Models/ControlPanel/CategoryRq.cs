
using System.Text.Json.Serialization;
namespace Rawaa_Api.Models.ControlPanel
{
    public class CategoryRq
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
    }

    public class CategorySearch
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
    }


}
