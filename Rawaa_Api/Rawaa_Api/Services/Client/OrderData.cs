using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Models.Client;
using System.Security.Cryptography.X509Certificates;

namespace Rawaa_Api.Services.Client
{
    public class OrderData
    {
        RawaaDBContext context;
        public OrderData()
        {
            context = new RawaaDBContext();
        }
        // client
        public Order Add(Order model)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var orderDetails = new OrderDetail();
            decimal price = 0.0M;
            foreach (var item in model.OrderDetails)
            {
                var product = context.Products.Find(item.ProductId);
                var priceQuantity = 0.0M;
                switch (item.Size)
                {
                    case 1:
                        priceQuantity += (decimal)product.SmallSizePrice * (decimal)item.Quantity;
                        item.ProductPrice = product.SmallSizePrice;
                        break;
                    case 2:
                        priceQuantity += (decimal)product.MediumSizePrice * (decimal)item.Quantity;
                        item.ProductPrice = product.MediumSizePrice;
                        break;
                    case 3:
                        priceQuantity += (decimal)product.BigSizePrice * (decimal)item.Quantity;
                        item.ProductPrice = product.BigSizePrice;
                        break;
                }

                price += priceQuantity;
            }
            var deliveryFee = 10.0M;
            price += deliveryFee;

            model.Total = price;

            model.OrderNumber = DateTime.Now.ToString("yyMMddHHmmssff");
            var res = context.Orders.Add(model).Entity;
            context.SaveChanges();

            Remove((int)model.CustomerId);
            return res;

        }

        public Order? CancelOrder(OrderStatusRequest state)
        {
            var entity = context.Orders.Find(state.Id);

            if (entity == null)
                return null;

            entity.OrderStatus = state.OrderStatus;

            context.SaveChanges();
            entity.OrderDetails = null;

            return entity;
        }
        public List<Order> List(int userId)
        {
            var entity = context.Orders
                .Where(e => e.CustomerId == userId)
                .OrderByDescending(e => e.OrderDate)
                .ToList();
            return entity;
        }

        public object? OrderDetails(int? orderId, string lang)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var orderDetail = (from da in context.DeliveryAddresses
                               join o in context.Orders on da.Id equals o.DeliveryAddressId
                               where o.Id == orderId

                               select new Order
                               {
                                   Id = o.Id,
                                   OrderNumber = o.OrderNumber,
                                   OrderStatus = o.OrderStatus,
                                   PymentMethod = o.PymentMethod,
                                   DeliveryFee = o.DeliveryFee,
                                   DeliveryAddress = new DeliveryAddress() 
                                   { 
                                       Governorate = da.Governorate ,
                                       ShortName = da.ShortName,
                                       ApartmentNumber = da.ApartmentNumber,
                                       BuildingNumber = da.BuildingNumber,
                                       FloorrUmber = da.FloorrUmber,
                                       City = da.City,
                                       Street = da.Street,
                                       Id = da.Id
                                   }

                               }).FirstOrDefault();
            

            return orderDetail;
        }

        public object? ProductsInOrder(int? orderId, string lang)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var orderDetail = (from od in context.OrderDetails
                               join p in context.Products on od.ProductId equals p.Id
                               join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                               join l in context.LanguageNames on t.LanguageId equals l.Id
                               where l.Name == lang
                               where od.OrderId == orderId
                               select new
                               {
                                   Title = t.Title,
                                   Image = p.Image,
                                   ProductPrice = od.ProductPrice,
                                   Quantity = od.Quantity,
                                   Size = od.Size
                               }).ToList();
            return orderDetail;
        }

        // remove All items in cart of user
        private bool Remove(int userId)
        {
            var entity = context.Carts.Where(e =>
                         e.CustomerId == userId).ToList();
            if (entity == null)
                return false;

            context.Carts.RemoveRange(entity);
            context.SaveChanges();

            var res = context.Carts.Where(e =>
                        e.CustomerId == userId).Any();
            return !res;
        }

    }
}
