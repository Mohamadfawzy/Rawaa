using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class IngredientsTitleTranslation
    {
        public string? Title { get; set; }
        public int IngredientsId { get; set; }
        public int LanguageId { get; set; }

        public virtual Ingredient Ingredients { get; set; } = null!;
        public virtual LanguageName Language { get; set; } = null!;
    }
}
