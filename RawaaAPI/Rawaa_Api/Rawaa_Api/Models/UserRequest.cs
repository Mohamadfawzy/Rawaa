
namespace Rawaa_Api.Models
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string? Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; } 
        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }

    }
}
