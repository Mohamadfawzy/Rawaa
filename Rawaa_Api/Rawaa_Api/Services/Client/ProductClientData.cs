using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Models.Client;

namespace Rawaa_Api.Services.Client
{
    public class ProductClientData
    {
        RawaaDBContext context;

        // ctor client 
        public ProductClientData()
        {
            context = new RawaaDBContext();
        }

        public ProductRequestClinet Find(int? id , string lang)
        {
            var products = new List<ProductRequestClinet>();
            products = (from p in context.Products
                        join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where t.ProductId == id && l.Name == lang
                        select new ProductRequestClinet
                        {
                            Id = p.Id,
                            Image = p.Image,
                            Title = t.Title,
                            SmallSizePrice = p.SmallSizePrice,
                            MediumSizePrice = p.MediumSizePrice,
                            BigSizePrice = p.BigSizePrice,
                            DiscountValue = p.DiscountValue,
                            Calories = p.Calories,
                            HasTaste = p.HasTaste,
                            CategoryId = p.CategoryId,
                        }).ToList();

            return products.FirstOrDefault();
        }
        public IList<ProductRequestClinet> List(string lang, int categoryId)
        {
            var products = new List<ProductRequestClinet>();
            products = (from p in context.Products
                        join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                        join l in context.LanguageNames on t.LanguageId equals l.Id
                        where l.Name == lang && p.CategoryId ==categoryId
                        select new ProductRequestClinet

                        {
                            Id = p.Id,
                            Image = p.Image,
                            Title = t.Title,
                            SmallSizePrice = p.SmallSizePrice,
                            MediumSizePrice = p.MediumSizePrice,
                            BigSizePrice = p.BigSizePrice,
                            DiscountValue = p.DiscountValue,
                            Calories = p.Calories,
                            HasTaste = p.HasTaste,
                            CategoryId = p.CategoryId,
                        }).ToList();

            return products;
        }

        public object Max(string lang)
        {
            var products = new List<ProductRequestClinet>();
            var time = DateTime.Now.Date.AddDays(-2);
            var res = context.OrderDetails.GroupBy(x => x.ProductId)
                .Select(g => new { productId = g.Key, count = g.Count(c => c.CreateOn >= time) })
                .Where(c => c.count >= 2)
                .ToList();




            products = (from p in context.Products
                        join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                        join l in context.LanguageNames on t.LanguageId equals l.Id

                        where l.Name == lang 

                        select new ProductRequestClinet

                        {
                            Id = p.Id,
                            Image = p.Image,
                            Title = t.Title,
                            SmallSizePrice = p.SmallSizePrice,
                            MediumSizePrice = p.MediumSizePrice,
                            BigSizePrice = p.BigSizePrice,
                            DiscountValue = p.DiscountValue,
                            Calories = p.Calories,
                            HasTaste = p.HasTaste,
                            CategoryId = p.CategoryId,
                        }).ToList();

            return res;
        }


        public List<ProductRequestClinet> Search(string searchString, string lang)
        {
            var products = (from t in context.ProductTitleTranslations
                            where t.Title.Contains(searchString)
                            select t

                            into title

                            join p in context.Products on title.ProductId equals p.Id
                            select new ProductRequestClinet
                            {
                                Title = title.Title,
                                Id = p.Id,
                                Image = p.Image,
                                SmallSizePrice = p.SmallSizePrice,
                                MediumSizePrice = p.MediumSizePrice,
                                BigSizePrice = p.BigSizePrice,
                                DiscountValue = p.DiscountValue,
                                Calories = p.Calories,
                                HasTaste = p.HasTaste,
                                CategoryId = p.CategoryId,
                            }).ToList();

            return products;

        }

    }
}
