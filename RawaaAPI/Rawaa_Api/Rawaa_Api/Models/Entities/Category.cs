using System;
using System.Collections.Generic;

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

        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<CategorieTitleTranslation> CategorieTitleTranslations { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
