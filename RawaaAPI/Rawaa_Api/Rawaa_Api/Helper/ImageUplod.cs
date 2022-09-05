using System.ComponentModel.DataAnnotations.Schema;

namespace Rawaa_Api.Helper
{
    [NotMapped]
    public class ImageUplod
    {
        public IFormFile? Files { get; set; } 
    }
}
