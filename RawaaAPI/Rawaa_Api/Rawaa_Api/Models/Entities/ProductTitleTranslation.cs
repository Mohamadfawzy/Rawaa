﻿using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class ProductTitleTranslation
    {
        public string? Title { get; set; }
        public int LanguageId { get; set; }
        public int ProductId { get; set; }

        public virtual LanguageName Language { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
