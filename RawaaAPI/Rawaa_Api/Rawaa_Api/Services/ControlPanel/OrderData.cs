using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
// cp
namespace Rawaa_Api.Services.ControlPanel
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
            // genirate order Number

            model.OrderNumber = DateTime.Now.ToString("yyMMddHHmmssff");
            var res = context.Orders.Add(model).Entity;
            context.SaveChanges();
            return res;


            //var order = new Order();
            //order.OrderStatus = 1;
            //order.CustomerId = model.CustomerId;
            //order.DeliveryAddressId = model.DeliveryAddressId;
            //order.PymentMethod = model.PymentMethod;

            //var orderDetails = new OrderDetail();
            //orderDetails.ProductId = model.ProductId;
            //orderDetails.Taste = model.Taste;
            //orderDetails.Size = model.Size;
            //orderDetails.Quantity = model.Quantity;

            //order.OrderDetails = orderDetails;
        }

        public Order? UpdateOrder(OrderStatusRequest state)
        {
            var entity = context.Orders.Find(state.Id);

            if (entity == null)
                return null;

            entity.OrderStatus = state.OrderStatus;

            context.SaveChanges();
            entity.OrderDetails = null;

            return entity;
        }
        
        public object TotalPrice()
        {
            var dateCriteria = DateTime.Now.Date.AddDays(-1);

            var QueryDeatils = from o in context.Orders
                               where o.OrderDate >= dateCriteria
                               select new
                               {
                                   MerchantId =o.Total
                               };

            var thisDay = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-1)).Sum(e=>e.Total);
            var yesterday = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-2)).Sum(e=>e.Total);
            var lastWeek = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-7)).Sum(e=>e.Total);
            var LastMonth = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-30)).Sum(e=>e.Total);
            var s = new { day = thisDay, yesterday = yesterday, lastWeek = lastWeek, LastMonth = LastMonth };
            return s;
        }


        public List<Order> List(int state, int pageNumber = 1, int pageSize = 10,int day = 1)
        {
            var date = DateTime.Now.Date.AddDays(-day);
            var list = context.Orders.Where(e => 
                        e.OrderStatus == state && e.OrderDate >= date)
                        .Skip((pageNumber -1) * pageSize)
                        .Take(pageSize)
                        .OrderByDescending(d=>d.OrderDate)
                        .ToList();
            return list;
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
                                       Governorate = da.Governorate,
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
    }
}
