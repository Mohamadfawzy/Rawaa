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
            return context.Restaurants.Add(entity).Entity;
        }

        public Restaurant Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IList<Restaurant> List(string lang)
        {
            return context.Restaurants.ToList();
        }

        public List<Restaurant> Search(string searchString)
        {
            throw new NotImplementedException();
        }

        public Restaurant Update(int id, Restaurant model, string lang, bool udateImage = false)
        {
            var entity = context.Restaurants.AsNoTracking().SingleOrDefault(b => b.Id == id);
            var result = context.Update(entity).Entity;
            return result;
        }

        public Restaurant Delete(int id)
        {
            throw new NotImplementedException();
        }

        // data



    }
}
