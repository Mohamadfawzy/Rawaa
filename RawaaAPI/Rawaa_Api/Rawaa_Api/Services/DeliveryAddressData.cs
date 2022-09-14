using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public Customer? Delete(int id)
        {
            var entity = context.Customers.Find(id);
            if (entity == null)
                return null;

            context.SaveChanges();

            return entity;
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
