using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Rawaa_Api.Models.Entities;
// Data Source=rawaaDB.mssql.somee.com;Initial Catalog=rawaaDB;user id=mfhelal12345_SQLLogin_1; pwd=jlgjukukt9;
//Data Source=DESKTOP-UND6KAU\\SQLEXPRESS;Initial Catalog=test; Integrated Security=True
namespace Rawaa_Api.Models
{
    public partial class RawaaDBContext : DbContext
    {
        public RawaaDBContext()
        {
        }

        public RawaaDBContext(DbContextOptions<RawaaDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ad> Ads { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CategorieTitleTranslation> CategorieTitleTranslations { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; } = null!;
        public virtual DbSet<Drink> Drinks { get; set; } = null!;
        public virtual DbSet<DrinksTitleTranslation> DrinksTitleTranslations { get; set; } = null!;
        public virtual DbSet<Favorite> Favorites { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientsTitleTranslation> IngredientsTitleTranslations { get; set; } = null!;
        public virtual DbSet<LanguageName> LanguageNames { get; set; } = null!;
        public virtual DbSet<MealExtra> MealExtras { get; set; } = null!;
        public virtual DbSet<MealExtrasTitleTranslation> MealExtrasTitleTranslations { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductTitleTranslation> ProductTitleTranslations { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Staff> Staffs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=rawaaDB.mssql.somee.com;Initial Catalog=rawaaDB;user id=mfhelal12345_SQLLogin_1; pwd=jlgjukukt9;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategorieId).HasColumnName("categorie_id");

                entity.Property(e => e.Image)
                    .HasMaxLength(250)
                    .HasColumnName("image");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Categorie)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.CategorieId)
                    .HasConstraintName("FK_Ads_categorie_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Ads_product_id");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId })
                    .HasName("PK__cart__8915EC5AEFA8F3F4");

                entity.ToTable("cart");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CreateOn)
                    .HasColumnType("date")
                    .HasColumnName("create_on")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_cart_customer_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_cart_product_id");
            });

            modelBuilder.Entity<CategorieTitleTranslation>(entity =>
            {
                entity.HasKey(e => new { e.CategorieId, e.LanguageId })
                    .HasName("PK__Categori__7DE1DE54A5B954D8");

                entity.ToTable("Categorie_title_translation");

                entity.Property(e => e.CategorieId).HasColumnName("categorie_id");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.HasOne(d => d.Categorie)
                    .WithMany(p => p.CategorieTitleTranslations)
                    .HasForeignKey(d => d.CategorieId)
                    .HasConstraintName("FK_Categorie_title_translation_categorie_id");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.CategorieTitleTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Categorie_title_translation_language_id");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("image");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(70)
                    .HasColumnName("full_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<DeliveryAddress>(entity =>
            {
                entity.ToTable("DeliveryAddress");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApartmentNumber).HasColumnName("apartment_number");

                entity.Property(e => e.BuildingNumber).HasColumnName("building_number");

                entity.Property(e => e.CityAr)
                    .HasMaxLength(100)
                    .HasColumnName("city_ar");

                entity.Property(e => e.CityEn)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("city_en");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.FloorrUmber).HasColumnName("floorr_umber");

                entity.Property(e => e.GovernorateAr)
                    .HasMaxLength(100)
                    .HasColumnName("governorate_ar");

                entity.Property(e => e.GovernorateEn)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("governorate_en");

                entity.Property(e => e.Notes)
                    .HasMaxLength(300)
                    .HasColumnName("notes");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(100)
                    .HasColumnName("short_name");

                entity.Property(e => e.StreetAr)
                    .HasMaxLength(100)
                    .HasColumnName("street_ar");

                entity.Property(e => e.StreetEn)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("street_en");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.DeliveryAddresses)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_deliveryAddress_customer_id");
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<DrinksTitleTranslation>(entity =>
            {
                entity.HasKey(e => new { e.DrinksId, e.LanguageId })
                    .HasName("PK__Drinks_t__A5110794AE7758DB");

                entity.ToTable("Drinks_title_translation");

                entity.Property(e => e.DrinksId).HasColumnName("drinks_id");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasColumnName("title");

                entity.HasOne(d => d.Drinks)
                    .WithMany(p => p.DrinksTitleTranslations)
                    .HasForeignKey(d => d.DrinksId)
                    .HasConstraintName("FK_Drinks_title_translation_drinks_id");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.DrinksTitleTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Drinks_title_translation_language_id");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId })
                    .HasName("PK__Favorite__8915EC5A8FC532E1");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("customer_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.SavedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("saved_on")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favorites_customer_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_favorites_product_id");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");
            });

            modelBuilder.Entity<IngredientsTitleTranslation>(entity =>
            {
                entity.HasKey(e => new { e.IngredientsId, e.LanguageId })
                    .HasName("PK__Ingredie__4106C5241FE9D014");

                entity.ToTable("Ingredients_title_translation");

                entity.Property(e => e.IngredientsId).HasColumnName("ingredients_id");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasColumnName("title");

                entity.HasOne(d => d.Ingredients)
                    .WithMany(p => p.IngredientsTitleTranslations)
                    .HasForeignKey(d => d.IngredientsId)
                    .HasConstraintName("FK_Ingredients_title_translation_ingredients_id");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.IngredientsTitleTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Ingredients_title_translation_language_id");
            });

            modelBuilder.Entity<LanguageName>(entity =>
            {
                entity.ToTable("Language_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(70)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<MealExtra>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<MealExtrasTitleTranslation>(entity =>
            {
                entity.HasKey(e => new { e.MealExtrasId, e.LanguageId })
                    .HasName("PK__MealExtr__B5FBE33E4BDB8649");

                entity.ToTable("MealExtras_title_translation");

                entity.Property(e => e.MealExtrasId).HasColumnName("mealExtras_id");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasColumnName("title");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.MealExtrasTitleTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_MealExtras_title_translation_language_id");

                entity.HasOne(d => d.MealExtras)
                    .WithMany(p => p.MealExtrasTitleTranslations)
                    .HasForeignKey(d => d.MealExtrasId)
                    .HasConstraintName("FK_MealExtras_title_translation_mealExtras_id");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.OrderNumber, "UQ__Orders__CAC5E74330E9DB6B")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomersId).HasColumnName("customers_id");

                entity.Property(e => e.DeliveryAddressId).HasColumnName("deliveryAddress_id");

                entity.Property(e => e.DeliveryFee)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("delivery_fee");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.PymentMethod).HasColumnName("pyment_method");

                entity.Property(e => e.RestaurantId)
                    .HasColumnName("restaurant_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.Customers)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomersId)
                    .HasConstraintName("FK_order_customers_id");

                entity.HasOne(d => d.DeliveryAddress)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_deliveryAddress_id");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_order_restaurant_id");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.OrderId })
                    .HasName("PK__Order_De__C367EBD7F1AD2462");

                entity.ToTable("Order_Details");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CreateOn)
                    .HasColumnType("datetime")
                    .HasColumnName("create_on")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DrinkId)
                    .HasColumnName("drink_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Taste).HasColumnName("taste");

                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.DrinkId)
                    .HasConstraintName("FK_order_details_drink_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_order_details_order_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_detail_product_id");

                entity.HasMany(d => d.MealExtras)
                    .WithMany(p => p.OrderDetails)
                    .UsingEntity<Dictionary<string, object>>(
                        "OrderItemsMealExtra",
                        l => l.HasOne<MealExtra>().WithMany().HasForeignKey("MealExtraId").HasConstraintName("FK_orderItems_mealExtras_mealExtra_id"),
                        r => r.HasOne<OrderDetail>().WithMany().HasForeignKey("ProductId", "OrderId").HasConstraintName("FK_orderItems_mealExtras_product_id_order_id"),
                        j =>
                        {
                            j.HasKey("ProductId", "OrderId", "MealExtraId").HasName("PK__OrderIte__814E11760EB6F35C");

                            j.ToTable("OrderItems_MealExtras");

                            j.IndexerProperty<int>("ProductId").HasColumnName("product_id");

                            j.IndexerProperty<int>("OrderId").HasColumnName("order_id");

                            j.IndexerProperty<int>("MealExtraId").HasColumnName("mealExtra_id");
                        });
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BigSizePrice)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("big_size_price");

                entity.Property(e => e.Calories).HasColumnName("calories");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.DiscountValue)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("discount_value");

                entity.Property(e => e.HasTaste).HasColumnName("has_taste");

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.MediumSizePrice)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("Medium_size_price");

                entity.Property(e => e.SmallSizePrice)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("small_size_price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_products_category_id");

                entity.HasMany(d => d.Ingredients)
                    .WithMany(p => p.Products)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProductsIngredient",
                        l => l.HasOne<Ingredient>().WithMany().HasForeignKey("IngredientId").HasConstraintName("FK_products_ingredients_ingredient_id"),
                        r => r.HasOne<Product>().WithMany().HasForeignKey("ProductId").HasConstraintName("FK_products_ingredients_product_id"),
                        j =>
                        {
                            j.HasKey("ProductId", "IngredientId").HasName("PK__products__AC0C38C9DACD286F");

                            j.ToTable("products_ingredients");

                            j.IndexerProperty<int>("ProductId").HasColumnName("product_id");

                            j.IndexerProperty<int>("IngredientId").HasColumnName("ingredient_id");
                        });
            });

            modelBuilder.Entity<ProductTitleTranslation>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.LanguageId })
                    .HasName("PK__Product___6F06B29E6AA21871");

                entity.ToTable("Product_title_translation");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.LanguageId).HasColumnName("language_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasColumnName("title");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.ProductTitleTranslations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Product_title_translation_language_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTitleTranslations)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Product_title_translation_product_id");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Governorate)
                    .HasMaxLength(50)
                    .HasColumnName("governorate");

                entity.Property(e => e.NameAr)
                    .HasMaxLength(100)
                    .HasColumnName("name_ar");

                entity.Property(e => e.NameEn)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name_en");

                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Street)
                    .HasMaxLength(60)
                    .HasColumnName("street");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staffs");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.FullName)
                    .HasMaxLength(60)
                    .HasColumnName("full_name");

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.RestaurantId)
                    .HasColumnName("restaurant_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_staffs_Id");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_staffs_restaurant_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
