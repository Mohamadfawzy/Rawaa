using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Helper
{
    [NotMapped]
    public class Photo
    {

        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public byte[]? Bytes { get; set; }
        [JsonIgnore]
        public string? Description { get; set; }
        [JsonIgnore]
        public string? FileExtension { get; set; }
        [JsonIgnore]
        public decimal Size { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [FromForm]
        [NotMapped]
        public IFormFileCollection File { get; set; }
    }
}
