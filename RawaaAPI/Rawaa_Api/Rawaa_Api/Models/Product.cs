using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rawaa_Api.Models
{
    public partial class Product
    {
        public Product()
        {
            Ads = new HashSet<Ad>();
            Carts = new HashSet<Cart>();
            Favorites = new HashSet<Favorite>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductTitleTranslations = new HashSet<ProductTitleTranslation>();
            Ingredients = new HashSet<Ingredient>();
        }

        public int Id { get; set; }
        public string? Image { get; set; }
        public decimal SmallSizePrice { get; set; }
        public decimal? MediumSizePrice { get; set; }
        public decimal? BigSizePrice { get; set; }
        public decimal? DiscountValue { get; set; }
        public short? Calories { get; set; }
        public byte? HasTaste { get; set; }
        public int? CategoryId { get; set; }

        [NotMapped]
        public string? Title { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductTitleTranslation> ProductTitleTranslations { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
