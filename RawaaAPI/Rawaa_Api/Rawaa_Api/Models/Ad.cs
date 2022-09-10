using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class Ad
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public int? CategorieId { get; set; }
        public int? ProductId { get; set; }

        public virtual Category? Categorie { get; set; }
        public virtual Product? Product { get; set; }
    }
}
