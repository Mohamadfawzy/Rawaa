using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Rawaa_Api.Helper
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        [Column("FirstName")]
        public string? First_Name { get; set; }
        [Required, MaxLength(200)]
        public string? LastName { get; set; }
        public byte[]? imageSource { get; set; }
    }
}
