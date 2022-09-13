using System;
using System.Collections.Generic;

namespace Rawaa_Api.Models.Entities
{
    public partial class MealExtra
    {
        public MealExtra()
        {
            MealExtrasTitleTranslations = new HashSet<MealExtrasTitleTranslation>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<MealExtrasTitleTranslation> MealExtrasTitleTranslations { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
