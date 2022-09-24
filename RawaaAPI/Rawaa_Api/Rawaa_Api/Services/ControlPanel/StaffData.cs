using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using Rawaa_Api.Helper;

namespace Rawaa_Api.Services.ControlPanel
{
    public class StaffData
    {
        RawaaDBContext context;

        public StaffData()
        {
            context = new RawaaDBContext();
        }

        public bool checkUserName(string userName)
        {
            return context.Staffs.Where(un => un.UserName == userName).Any();
        }

        public Staff Add(Staff model)
        {
            var res = context.Staffs.Add(model).Entity;
            context.SaveChanges();
            return res;
        }
        
        // login
        public Staff Login(StaffRequest model)
        {
           
            var res = context.Staffs.Where(e => e.UserName == model.UserName && e.Password == model.Password).FirstOrDefault();
            
            return res;
        }

        // find
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

        // list
        public List<Staff> List(string lang)
        {
            var entity = context.Staffs.ToList();

            return entity;
        }

        // search
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

        // update 
        public StaffRequest? Update(int id, Staff model)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var entity = context.Staffs.Find(id);

            if (entity == null)
                return null;
            model.Id = id;
            //model.CreateOn = DateTime.Now;
            model.CreateOn = entity.CreateOn;
            model.UpdateOn = DateTime.Now;
            var res = context.Update(model);
            context.Entry(model).Property(p => p.CreateOn).IsModified = false;
            context.SaveChanges();
            //model = Find(id);
            StaffRequest? staffInfo = JsonConvert.DeserializeObject<StaffRequest>(JsonConvert.SerializeObject(entity));
            
            var afrer = Find(id);
            return afrer;

        }

        public Staff? Delete(int id)
        {
            var entity = context.Staffs.Find(id);
            if (entity == null)
                return null;
            if (entity.UserName == "admin")
                return null;
            context.Remove(entity);
            context.SaveChanges();

            return entity;
        }


    }
}
