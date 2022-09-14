using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.Interfaces;
using System.Linq;
namespace Rawaa_Api.Services.ControlPanel
{
    public class RestaurantData : IProvider<Restaurant>
    {
        RawaaDBContext context;
        
        // ctor
        public RestaurantData()
        {
            context = new RawaaDBContext();
        }

        public Restaurant Add(Restaurant entity)
        {
            var result = context.Restaurants.Add(entity).Entity;
            context.SaveChanges();
            return result;

        }

        public IList<Restaurant> List(string lang)
        {

            return context.Restaurants.ToList();
        }

      

        public Restaurant Update(int id, Restaurant model, string lang, bool udateImage = false)
        {
            var entity = context.Restaurants.AsNoTracking().SingleOrDefault(b => b.Id == id);
            model.Id = id;
            var result = context.Update(model).Entity;
            context.SaveChanges();
            return result;
        }

        public Restaurant Find(int? id)
        {
            return null;
        }

        public Restaurant Delete(int id)
        {
            return null;
        }
       
        public List<Restaurant> Search(string searchString)
        {
            return null;
        }


    }
}
