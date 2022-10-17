using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class LanguageName
    {
        public LanguageName()
        {
            CategorieTitleTranslations = new HashSet<CategorieTitleTranslation>();
            DrinksTitleTranslations = new HashSet<DrinksTitleTranslation>();
            IngredientsTitleTranslations = new HashSet<IngredientsTitleTranslation>();
            MealExtrasTitleTranslations = new HashSet<MealExtrasTitleTranslation>();
            ProductTitleTranslations = new HashSet<ProductTitleTranslation>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<CategorieTitleTranslation> CategorieTitleTranslations { get; set; }
        public virtual ICollection<DrinksTitleTranslation> DrinksTitleTranslations { get; set; }
        public virtual ICollection<IngredientsTitleTranslation> IngredientsTitleTranslations { get; set; }
        public virtual ICollection<MealExtrasTitleTranslation> MealExtrasTitleTranslations { get; set; }
        public virtual ICollection<ProductTitleTranslation> ProductTitleTranslations { get; set; }
    }
}
