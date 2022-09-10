using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models
{
    public partial class MealExtrasTitleTranslation
    {
        public string? Title { get; set; }
        public int MealExtrasId { get; set; }
        public int LanguageId { get; set; }

        public virtual LanguageName Language { get; set; } = null!;
        public virtual MealExtra MealExtras { get; set; } = null!;
    }
}
