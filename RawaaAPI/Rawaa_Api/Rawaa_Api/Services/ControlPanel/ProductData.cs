using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Rawaa_Api.Services.ControlPanel
{
    public class ProductData
    {
        RawaaDBContext context;
        public static IWebHostEnvironment _webHostEnvironment;
        FileProcessor fileProcessor;
        
        // ctor
        public ProductData(IWebHostEnvironment web)
        {
            context = new RawaaDBContext();
            _webHostEnvironment = web;
            fileProcessor = new FileProcessor(_webHostEnvironment);
        }

        public ProductRequest Add(ProductRequest model)
        {
            var listOfTitle = new List<ProductTitleTranslation>();

            var product = new Product
            {
                Image = "P_" + Guid.NewGuid().ToString() + model.Image,
                SmallSizePrice = model.SmallSizePrice,
                MediumSizePrice =model.MediumSizePrice,
                BigSizePrice=model.BigSizePrice,
                DiscountValue=model.DiscountValue,
                Calories=model.Calories,
                HasTaste=model.HasTaste,
                CategoryId = model.CategoryId,
                ProductTitleTranslations = listOfTitle
            };

            listOfTitle.Add(new ProductTitleTranslation { Title = model.TitleAr, LanguageId = 1 });

            if (!string.IsNullOrEmpty(model.TitleEn))
                listOfTitle.Add(new ProductTitleTranslation { Title = model.TitleEn, LanguageId = 2 });

            var result = context.Products.Add(product).Entity;

            context.SaveChanges();

            model.Id = result.Id;
            model.Image = result.Image;
            return model;
        }

        public ProductRequest Update(int id, ProductRequest model, string lang, bool isThereImage)
        {
            var listOfTitle = new List<ProductTitleTranslation>();

            var entity = context.Products.AsNoTracking().SingleOrDefault(p => p.Id == id);

            if (entity == null)
                return null;

            if (string.IsNullOrEmpty(entity.Image) && isThereImage)
                model.Image = "C_" + Guid.NewGuid().ToString() + model.Image;
            else if(!string.IsNullOrEmpty(entity.Image) && isThereImage)
            {
                var newNameImage = Path.ChangeExtension(entity.Image, model.Image);
                model.Image = newNameImage;
            }


            listOfTitle.Add(new ProductTitleTranslation { Title = model.TitleAr, ProductId = id, LanguageId = 1 });
            listOfTitle.Add(new ProductTitleTranslation { Title = model.TitleEn, ProductId = id, LanguageId = 2 });


            entity.Image = model.Image;// model.Image;
            entity.SmallSizePrice = model.SmallSizePrice;
            entity.MediumSizePrice = model.MediumSizePrice;
            entity.BigSizePrice = model.BigSizePrice;
            entity.DiscountValue = model.DiscountValue;
            entity.Calories = model.Calories;
            entity.HasTaste = model.HasTaste;
            entity.CategoryId = model.CategoryId;

            context.CategorieTitleTranslations.AsNoTracking().Where(t => t.CategorieId == id).ToList();
            context.UpdateRange(listOfTitle);

            context.Update(entity).Property(p=>p.Image).IsModified = isThereImage; //Entry(entity).CurrentValues.SetValues(category);
            context.SaveChanges();


            model = Find(id);
            return model;

        }

        public ProductRequest Delete(int id)
        {
            var entity = context.Products.Find(id);
            if (entity == null)
                return null;
            var result = context.Remove(entity);
            context.SaveChanges();

            return new ProductRequest() { Id = result.Entity.Id, Image = entity.Image };
        }

        public ProductRequest Find(int? id)
        {
            var products = new List<ProductRequest>();
            products = (from p in context.Products
                        join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where t.ProductId == id && t.LanguageId == 1
                        select new ProductRequest 
                        {
                            Id = p.Id,
                            Image = p.Image,
                            TitleAr = t.Title,
                            SmallSizePrice = p.SmallSizePrice,
                            MediumSizePrice = p.MediumSizePrice,
                            BigSizePrice = p.BigSizePrice,
                            DiscountValue = p.DiscountValue,
                            Calories = p.Calories,
                            HasTaste = p.HasTaste,
                            CategoryId = p.CategoryId,
                        }

                        into ar

                        join en in context.ProductTitleTranslations on ar.Id equals en.ProductId
                        where en.LanguageId == 2 && ar.Id == en.ProductId

                        select new ProductRequest
                        {
                            Id = ar.Id,
                            Image = ar.Image,
                            TitleAr = ar.TitleAr,
                            TitleEn = en.Title,
                            SmallSizePrice = ar.SmallSizePrice,
                            MediumSizePrice = ar.MediumSizePrice,
                            BigSizePrice = ar.BigSizePrice,
                            DiscountValue = ar.DiscountValue,
                            Calories = ar.Calories,
                            HasTaste = ar.HasTaste,
                            CategoryId = ar.CategoryId,

                        }).ToList();
            return products.FirstOrDefault();
        }
        public IList<ProductRequest> List(string lang)
        {
            var products = new List<ProductRequest>();
            products = (from p in context.Products
                        join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where t.LanguageId == 1
                        select new ProductRequest
                        {
                            Id = p.Id,
                            Image = p.Image,
                            TitleAr = t.Title,
                            SmallSizePrice = p.SmallSizePrice,
                            MediumSizePrice = p.MediumSizePrice,
                            BigSizePrice = p.BigSizePrice,
                            DiscountValue = p.DiscountValue,
                            Calories = p.Calories,
                            HasTaste = p.HasTaste,
                            CategoryId = p.CategoryId,
                        }

                        into ar

                        join en in context.ProductTitleTranslations on ar.Id equals en.ProductId
                        where en.LanguageId == 2


                        select new ProductRequest
                        {
                            Id = ar.Id,
                            Image = ar.Image,
                            TitleAr = ar.TitleAr,
                            TitleEn = en.Title,
                            SmallSizePrice = ar.SmallSizePrice,
                            MediumSizePrice = ar.MediumSizePrice,
                            BigSizePrice = ar.BigSizePrice,
                            DiscountValue = ar.DiscountValue,
                            Calories = ar.Calories,
                            HasTaste = ar.HasTaste,
                            CategoryId = ar.CategoryId,

                        }).ToList();
            return products;
        }

        public List<ProductRequest> Search(string searchString)
        {
            var products = (from t in context.ProductTitleTranslations
                            where t.Title.Contains(searchString)
                            select t

                            into title

                            join c in context.Products on title.ProductId equals c.Id
                            select new ProductRequest
                            {
                                Title = title.Title,
                                Id = c.Id,
                                Image = c.Image,
                                SmallSizePrice = c.SmallSizePrice,
                                MediumSizePrice = c.MediumSizePrice,
                                BigSizePrice = c.BigSizePrice,
                                DiscountValue = c.DiscountValue,
                                Calories = c.Calories,
                                HasTaste = c.HasTaste,
                                CategoryId = c.CategoryId,
                            }).ToList();

            return products;

        }

    }
}
