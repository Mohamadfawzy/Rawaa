using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;

namespace Rawaa_Api.Services.ControlPanel
{
    public class ProductData : IProductData
    {
        RawaaDBContext context;
        private readonly IMapper mapper;

        // ctor
        public ProductData(IMapper mapper)
        {
            context = new RawaaDBContext();
            this.mapper = mapper;
        }

        public ProductRequest Add(ProductRequest model, string imageExtension)
        {
            var product = mapper.Map<Product>(model);
            product.Image = CreateNewImageName(imageExtension);
            if (model.TitleAr is not null)
                product.ProductTitleTranslations.Add(new ProductTitleTranslation(model.TitleAr, 1));
            if (model.TitleEn is not null)
                product.ProductTitleTranslations.Add(new ProductTitleTranslation(model.TitleEn, 2));

            var result = context.Products.Add(product).Entity;

            var res = context.SaveChanges();
            if (res > 0)
                return Find(result.Id);

            return mapper.Map<ProductRequest>(result);
        }

        public ProductRequest Update(int id, ProductRequest model, string lang, bool isThereImage)
        {
            var listOfTitle = new List<ProductTitleTranslation>();

            var entity = context.Products.AsNoTracking().SingleOrDefault(p => p.Id == id);

            if (entity == null)
                return null;

            if (string.IsNullOrEmpty(entity.Image) && isThereImage)
                model.Image = "P_" + Guid.NewGuid().ToString() + model.Image;
            else if (!string.IsNullOrEmpty(entity.Image) && isThereImage)
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

            context.Update(entity).Property(p => p.Image).IsModified = isThereImage; //context.Entry(entity).CurrentValues.SetValues(category);
            context.SaveChanges();


            model = Find(id);
            return model;

        }

        public ProductRequest? UpdateImage(int id, string extension)
        {
            var entity = context.Products.AsNoTracking().SingleOrDefault(p => p.Id == id);
            var newNameImage = "";

            if (entity == null)
                return null;

            if (string.IsNullOrEmpty(entity.Image))
                newNameImage = "P_" + Guid.NewGuid().ToString() + extension;
            else if (!string.IsNullOrEmpty(entity.Image))
            {
                newNameImage = Path.ChangeExtension(entity.Image, extension);
            }
            entity.Image = newNameImage;
            context.Update(entity);
            context.Entry(entity).Property(p => p.SmallSizePrice).IsModified = false;
            context.Entry(entity).Property(p => p.MediumSizePrice).IsModified = false;
            context.Entry(entity).Property(p => p.BigSizePrice).IsModified = false;
            context.Entry(entity).Property(p => p.DiscountValue).IsModified = false;
            context.Entry(entity).Property(p => p.Calories).IsModified = false;
            context.Entry(entity).Property(p => p.HasTaste).IsModified = false;
            context.Entry(entity).Property(p => p.CategoryId).IsModified = false;
            context.Entry(entity).Property(p => p.CreateOn).IsModified = false;
            context.Entry(entity).Property(p => p.DiscountExpiryDate).IsModified = false;
            context.SaveChanges();
            var rs = Find(id);
            return rs;

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



        public bool DeleteRange(int[] range)
        {
            var products = context.Products.Where(t => range.Contains(t.Id)).Select(p=> new Product { Id = p.Id, Image = p.Image});
            context.Products.RemoveRange(products);
            return true;
        }

        //
        private string CreateNewImageName(string extension)
        {
            return "P_" + Guid.NewGuid().ToString() + extension;
        }

    }
}
