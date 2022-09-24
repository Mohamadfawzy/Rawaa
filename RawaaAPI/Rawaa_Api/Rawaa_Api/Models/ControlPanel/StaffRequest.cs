using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.ControlPanel
{
    public class StaffRequest
    {
        public int Id { get; set; }
        public string? FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public bool Active { get; set; }
        public string? Jop { get; set; }
        public int RestaurantId { get; set; }
        public int? ManagerId { get; set; }

        //
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public int? OrdersCount { get; set; }
    }
}
