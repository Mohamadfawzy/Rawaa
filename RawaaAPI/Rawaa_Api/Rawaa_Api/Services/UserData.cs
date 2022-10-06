using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Helper;

namespace Rawaa_Api.Services
{
    public class UserData
    {
        RawaaDBContext context;
        public UserData()
        {
            context = new RawaaDBContext();
        }
        // client
        public bool checkEmail(string email)
        {
            return context.Customers.Where(un => un.Email == email).Any();
        }
        public bool EmailValidity(string email,int id)
        {
            //var res = context.Customers.Where(un => un.Email == email && un.Id != id).Any();
            var res = context.Customers.Any(un => un.Email == email && un.Id != id);

            return res;
        }

        public Customer Add(Customer model)
        {
            var res = context.Customers.Add(model).Entity;
            context.SaveChanges();
            return res;
        }

        public UserRequest Login(UserRequest user)
        {
            var res = context.Customers.Where(e => e.Email == user.Email && e.Password == user.Password || e.Phone == user.Phone && e.Password == user.Password).FirstOrDefault();
            var result = CastClass.Deserialize(res,user);
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
            var result = CastClass.Deserialize( model,userInfo);
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
