using Microsoft.AspNetCore.Hosting;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Services
{
    public class ProductData : IProvider<Product>
    {
        RawaaDBContext context;
        Product defaultModel = new Product()
        {
            Id = 1,
            Title = "",
            Image = "",
            SmallSizePrice = 1,
            MediumSizePrice = 1,
            BigSizePrice = 1,
            DiscountValue = 1,
            Calories = 11,
            HasTaste = 1,
            CategoryId = 1
        };
        public static IWebHostEnvironment _webHostEnvironment;
        public ProductData(IWebHostEnvironment web)
        {
            context = new RawaaDBContext();
            _webHostEnvironment = web;
        }
        public Product Add(Product entity)
        {
            entity.Image = "P_"+Guid.NewGuid().ToString().Substring(0, 12); ;
            var result = context.Products.Add(entity);
            context.SaveChanges();
            var title = context.ProductTitleTranslations.Add(new ProductTitleTranslation { Title = entity.Title, ProductId = entity.Id });
            context.SaveChanges();
            return entity;
        }

        public Product Find(int? id)
        {
            var product = new Product();
            if (id is int)
            {
                product = (from p in context.Products
                           join translat in context.ProductTitleTranslations on p.Id equals translat.ProductId
                           //orderby translat.Id
                           where p.Id == id
                           select new Product
                           {
                               Id = p.Id,
                               Title = translat.Title,
                               Image = p.Image,
                               SmallSizePrice = p.SmallSizePrice,
                               MediumSizePrice = p.MediumSizePrice,
                               BigSizePrice = p.BigSizePrice,
                               DiscountValue = p.DiscountValue,
                               Calories = p.Calories,
                               HasTaste = p.HasTaste,
                               CategoryId = p.CategoryId
                           }).FirstOrDefault();
            }
            if (product != null)
            {
                return product;
            }
            else return null;
        }

        public IList<Product> List(string lang)
        {

            var products = new List<Product>();
            products = (from p in context.Products
                       join translat in context.ProductTitleTranslations on p.Id equals translat.ProductId
                       //orderby translat.Id
                       select new Product
                       {
                           Id = p.Id,
                           Title = translat.Title,
                           Image = p.Image,
                           SmallSizePrice = p.SmallSizePrice,
                           MediumSizePrice = p.MediumSizePrice,
                           BigSizePrice = p.BigSizePrice,
                           DiscountValue = p.DiscountValue,
                           Calories = p.Calories,
                           HasTaste = p.HasTaste,
                           CategoryId = p.CategoryId
                       }).ToList();

            if (products != null)
            {
                return products;
            }
            else return null;


            return context.Products.ToList<Product>();

        }

        public List<Product> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public Product Update(int id, Product entity, string lang, bool udateImage)
        {
            throw new NotImplementedException();
        }
        public Product Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
