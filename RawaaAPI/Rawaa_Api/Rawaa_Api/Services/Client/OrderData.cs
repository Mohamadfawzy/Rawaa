using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;

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
                        priceQuantity += (decimal)product.SmallSizePrice * (decimal)item.Quantity ;
                        break;
                    case 2:
                        priceQuantity += (decimal)product.MediumSizePrice * (decimal)item.Quantity;
                        break;
                    case 3:
                        priceQuantity += (decimal)product.BigSizePrice * (decimal)item.Quantity;
                        break;
                }

                price += priceQuantity;
            }
            var deliveryFee = 10.0;
            price += (decimal)deliveryFee;
            
            model.Total = (decimal)price;
            model.OrderNumber = "00000000000005";

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

        public UserRequest Login(UserRequest user)
        {
            var res = context.Customers.Where(e => e.Email == user.Email && e.Password == user.Password).FirstOrDefault();
            var result = CastClass.Deserialize(res, user);
            return result;
        }
        public UserRequest? Update(int id, Customer model)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var entity = context.Customers.Find(id);

            if (entity == null)
                return null;

            model.Id = id;
            model.CreateOn = entity.CreateOn;
            model.UpdateOn = DateTime.Now;

            var res = context.Update(model);
            context.Entry(model).Property(p => p.CreateOn).IsModified = false;
            context.SaveChanges();
            var userInfo = new UserRequest();
            var result = CastClass.Deserialize(model, userInfo);
            return result;

        }

        public Customer? Delete(int id, string pass)
        {
            var entity = context.Customers.Find(id);
            if (entity == null)
                return null;

            if (entity.Password == pass)
                context.Remove(entity);
            else
                return null;

            context.SaveChanges();

            return entity;
        }

        // cp NOT HANDEL
        public StaffRequest? Find(int? id)
        {
            //var staffInfo = new StaffRequest();
            var entity = context.Staffs.Find(id);

            var ordersCount = (from o in context.Orders
                               where o.StaffId == id
                               select o).Count();

            StaffRequest? staffInfo = JsonConvert.DeserializeObject<StaffRequest>(JsonConvert.SerializeObject(entity));

            if (ordersCount < 1)
                staffInfo.OrdersCount = 0;

            staffInfo.OrdersCount = ordersCount;
            return staffInfo;
        }
        public List<Staff> List()
        {
            var entity = context.Staffs.ToList();

            return entity;
        }

        public List<StaffRequest> Search(string searchString)
        {
            var staffs = (from s in context.Staffs
                          where s.FullName.Contains(searchString)
                          select new StaffRequest
                          {
                              Id = s.Id,
                              FullName = s.FullName,
                              UserName = s.UserName,
                              CreateOn = s.CreateOn,
                              Active = s.Active,
                              Jop = s.Jop,
                              RestaurantId = s.RestaurantId,
                              ManagerId = s.ManagerId,

                          }).ToList();
            return staffs;
        }




    }
}
