using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rawaa_Api.Helper
{
    [NotMapped]
    public class OldProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public DateTime ManufactoringDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        //navigation property: configure one-t0-many relationship with Photo 
        public List<Photo> Photos { get; set; }
        [FromForm]
        [NotMapped]
        public IFormFileCollection Files { get; set; }
    }
}
