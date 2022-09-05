using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            CategorieTitleTranslations = new HashSet<CategorieTitleTranslation>();
        }

        public int Id { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public string? Title { get; set; }

        
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategorieTitleTranslation> CategorieTitleTranslations { get; set; }
    }
}
