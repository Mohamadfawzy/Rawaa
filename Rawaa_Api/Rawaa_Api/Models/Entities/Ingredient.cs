using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientsTitleTranslations = new HashSet<IngredientsTitleTranslation>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public decimal? Price { get; set; }
        public int? ProductId { get; set; }

        public virtual ICollection<IngredientsTitleTranslation> IngredientsTitleTranslations { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
