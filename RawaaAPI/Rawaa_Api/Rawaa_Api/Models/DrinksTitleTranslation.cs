using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class DrinksTitleTranslation
    {
        public string? Title { get; set; }
        public int DrinksId { get; set; }
        public int LanguageId { get; set; }

        public virtual Drink Drinks { get; set; } = null!;
        public virtual LanguageName Language { get; set; } = null!;
    }
}
