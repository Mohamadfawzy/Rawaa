using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Models.Client;

namespace Rawaa_Api.Services.Client
{
    public class CartData
    {
        RawaaDBContext context;
        public CartData()
        {
            context = new RawaaDBContext();
        }

        // client
        public Cart Add(Cart model)
        {
            Cart res = new Cart();
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var entity = context.Carts.Where(e => e.CustomerId == model.CustomerId && e.ProductId == model.ProductId);

            if (entity.Any())
            {
                res = context.Update(model).Entity;
                context.Entry(model).Property(e => e.CreateOn).IsModified = false;

            }
            else
            {
                res = context.Carts.Add(model).Entity;
            }

            context.SaveChanges();
            return res;
        }

        // not call from url
        public Cart? Find(Cart mode)
        {
            return context.Carts.Where(e =>
                e.CustomerId == mode.CustomerId &&
                e.ProductId == mode.ProductId)
                .FirstOrDefault();
        }
        public List<CartResponseClient> List(int userId, string lang)
        {
            var entity = context.Carts.Where(e => e.CustomerId == userId).ToList();

            var products = (from c in context.Carts
                            join p in context.Products on c.ProductId equals p.Id
                            join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                            join l in context.LanguageNames on t.LanguageId equals l.Id
                            where l.Name == lang

                            select new CartResponseClient
                            {
                                CustomerId = c.CustomerId,
                                ProductId = p.Id,
                                Quantity = c.Quantity,
                                Taste = c.Taste,
                                Size = c.Size,
                                DrinkId = c.DrinkId,
                                CreateOn = c.CreateOn,
                                Product = new ProductRequestClinet()
                                {
                                    Title = t.Title,
                                    Id = p.Id,
                                    Image = p.Image,
                                    SmallSizePrice = p.SmallSizePrice,
                                    MediumSizePrice = p.MediumSizePrice,
                                    BigSizePrice = p.BigSizePrice,
                                    DiscountValue = p.DiscountValue,
                                    Calories = p.Calories,
                                    HasTaste = p.HasTaste,
                                    CategoryId = p.CategoryId
                                }
                            }).ToList();
            return products;
        }

        public bool Remove(int userId, int productId)
        {
            var entity = context.Carts.Where(e =>
                         e.CustomerId == userId &&
                         e.ProductId == productId).FirstOrDefault();
            if(entity != null)
            {
                context.Remove(entity);
                return true;
            }
            return false;
        }
    }
}
