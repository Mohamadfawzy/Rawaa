using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class CategorieTitleTranslation
    {
        public string Title { get; set; } = null!;
        public int CategorieId { get; set; }
        public int LanguageId { get; set; }

        public virtual Category Categorie { get; set; } = null!;
        public virtual LanguageName Language { get; set; } = null!;
    }
}
