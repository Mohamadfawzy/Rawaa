using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;

namespace Rawaa_Api.Services
{
    
    public class DeliveryAddressData
    {
        RawaaDBContext context;
        public DeliveryAddressData()
        {
            context = new RawaaDBContext();
        }

        // client
        public DeliveryAddress Add(DeliveryAddress model)
        {
            var res = context.DeliveryAddresses.Add(model).Entity;
            context.SaveChanges();
            return res;
        }

        public bool? Delete(int id)
        {
            var entity = context.DeliveryAddresses.Find(id);
            if (entity == null || entity.IsDeleted == true)
                return null;

            entity.IsDeleted = true;
            context.Update(entity);
            context.SaveChanges();
            //var res =context.DeliveryAddresses.Where(e => e.Id == id).Any();
            return false;
        }

        public List<DeliveryAddress> List(int userId)
        {
            var entity = context.DeliveryAddresses.Where(e => e.CustomerId == userId && e.IsDeleted != true).ToList();

            return entity;
        }

        public DeliveryAddress Find(int id)
        {
            var entity = context.DeliveryAddresses.Find(id);
            if (entity == null)
                return null;

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
