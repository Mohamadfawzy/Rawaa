using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Rawaa_Api.Helper
{
    [NotMapped]
    public class FileUplod
    {
        //public IFormFile files { get; set; }
        [JsonIgnore]
        public IFormFileCollection File { get; set; } = null!;


    }



}
