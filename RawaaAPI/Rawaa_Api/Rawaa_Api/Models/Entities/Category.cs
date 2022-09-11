using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models.Entities
{
    public partial class Category
    {
        public Category()
        {
            Ads = new HashSet<Ad>();
            CategorieTitleTranslations = new HashSet<CategorieTitleTranslation>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        public string? Title { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string? TitleEn { get; set; }

        [JsonIgnore]
        public virtual ICollection<Ad> Ads { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategorieTitleTranslation> CategorieTitleTranslations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
