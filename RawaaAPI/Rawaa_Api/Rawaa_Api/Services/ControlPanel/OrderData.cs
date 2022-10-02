using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Services.Client;
using System.Linq.Expressions;

// cp
namespace Rawaa_Api.Services.ControlPanel
{
    public class OrderData
    {
        RawaaDBContext context;

        // ctor
        public OrderData()
        {
            context = new RawaaDBContext();
        }

        // cp
        public Order? UpdateOrder(OrderStatusRequest model)
        {
            var entity = context.Orders.Find(model.Id);

            if (entity == null)
                return null;

            entity.OrderStatus = model.OrderStatus;

            context.SaveChanges();
            entity.OrderDetails = null;

            return entity;
        }

        // total price
        public object TotalPrice()
        {
            var dateCriteria = DateTime.Now.Date.AddDays(-1);

            var QueryDeatils = from o in context.Orders
                               where o.OrderDate >= dateCriteria
                               select new
                               {
                                   MerchantId = o.Total
                               };

            var thisDay = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-1)).Sum(e => e.Total);
            var yesterday = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-2)).Sum(e => e.Total);
            var lastWeek = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-7)).Sum(e => e.Total);
            var LastMonth = context.Orders.Where(e => e.OrderDate >= DateTime.Now.Date.AddDays(-30)).Sum(e => e.Total);
            var s = new { day = thisDay, yesterday = yesterday, lastWeek = lastWeek, LastMonth = LastMonth };
            return s;
        }

        // list of orders with paginate & datetime
        public List<Order> List(int state, int pageNumber = 1, int pageSize = 10, int day = 1)
        {
            var date = DateTime.Now.Date.AddDays(-day);
            var list = context.Orders.Where(e =>
                        e.OrderStatus == state && e.OrderDate >= date)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .OrderByDescending(d => d.OrderDate)
                        .ToList();
            return list;
        }

        // list wih user data and deivery address
        public List<Order> ListJoinUserData(int state, int pageNumber = 1, int pageSize = 10, int day = 1)
        {
            var date = DateTime.Now.Date.AddDays(-day);

            var result = (from o in context.Orders
                          where o.OrderStatus == state && o.OrderDate >= date

                          join da in context.DeliveryAddresses on o.DeliveryAddressId equals da.Id
                          join user in context.Customers on o.CustomerId equals user.Id

                          select new Order
                          {
                              Id = o.Id,
                              OrderNumber = o.OrderNumber,
                              Total = o.Total,
                              OrderStatus = o.OrderStatus,
                              PymentMethod = o.PymentMethod,
                              DeliveryFee = o.DeliveryFee,
                              CustomerId = o.CustomerId,
                              OrderDate = o.OrderDate,
                              DeliveryAddressId = o.DeliveryAddressId,
                              RestaurantId = o.RestaurantId,
                              StaffId = o.StaffId,

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
                              },
                              Customer = new Customer()
                              {
                                  Id = user.Id,
                                  FullName = user.FullName,
                                  Phone = user.Phone,
                                  Email = user.Email,
                              }
                          }).Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .OrderByDescending(d => d.OrderDate)

                            .ToList();


            //var orderDetails = (from or in context.Orders
            //                    join od in context.OrderDetails on or.Id equals od.OrderId

            //                    select
            //                        new OrderDetail
            //                        {
            //                            ProductId = od.ProductId,
            //                            OrderId = od.OrderId,
            //                            Size = od.Size,
            //                            Taste = od.Taste,
            //                            Quantity = od.Quantity,
            //                            ProductPrice = od.ProductPrice,
            //                            CreateOn = od.CreateOn,
            //                            DrinkId = od.DrinkId
            //                        }).ToList();

            return result;
        }
        public List<OrderDetail>? OrderDetails(int? orderId , string lang)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var orderDetails = (from o in context.Orders
                                join od in context.OrderDetails on o.Id equals od.OrderId
                                join p in context.Products on od.ProductId equals p.Id
                                join t in context.ProductTitleTranslations on p.Id equals t.ProductId
                                join l in context.LanguageNames on t.LanguageId equals l.Id
                                where l.Name == lang
                                where o.Id == orderId
                                select
                                new OrderDetail
                                {
                                    Product = new Product()
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
                                        CategoryId = p.CategoryId,
                                        CreateOn = p.CreateOn
                                    },
                                    ProductId = od.ProductId,
                                    OrderId = od.OrderId,
                                    Size = od.Size,
                                    Taste = od.Taste,
                                    Quantity = od.Quantity,
                                    ProductPrice = od.ProductPrice,
                                    CreateOn = od.CreateOn,
                                    DrinkId = od.DrinkId
                                }).ToList();


            return orderDetails;
        }

        public async Task<int>? NumberOfOrders(int day = 1, int state =1)
        {
            var date = DateTime.Now.Date.AddDays(-day);
            //if (state == 6)
            //    return;

            var o = new Order();
            Expression<Func<Order, bool>> Is42s = (v) => v.OrderDate >=date;

            var numberOfOrders = await context.Orders.CountAsync(Is42s);
            return numberOfOrders;
        }

       



        public async Task<int>? Aggregation(int day = 1)
        {
            var date = DateTime.Now.Date.AddDays(-day);
            var numberOfOrders = await context.Orders.CountAsync(c => c.OrderDate == date);
            return numberOfOrders;
        }
    }
}
