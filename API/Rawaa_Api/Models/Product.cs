﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rawaa_Api.Models
{
    public class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Favorites = new HashSet<Favorite>();
            OrderDetails = new HashSet<OrderDetail>();
            Ingredients = new HashSet<Ingredient>();
            ProductTitleTranslations = new HashSet<ProductTitleTranslation>();
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
        [JsonIgnore]
        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cart> Carts { get; set; }
        [JsonIgnore]
        public virtual ICollection<Favorite> Favorites { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        [JsonIgnore]
        public virtual ICollection<ProductTitleTranslation> ProductTitleTranslations { get; set; }
    }
}
