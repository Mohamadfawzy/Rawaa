using Rawaa_Api.Models;
using Rawaa_Api.Services.Interfaces;
using System.Linq;
namespace Rawaa_Api.Services
{
    public class RestaurantData : IProvider<Restaurant>
    {
        private RawaaDBContext context;
        public RestaurantData()
        {
            context = new RawaaDBContext();
            //context = _context;
        }
        public Restaurant Add(Restaurant entity)
        {
            var result = context.Add(entity);
            context.SaveChanges();

            //int id = entity.Id;
            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Restaurant Find(int? id)
        {
            var result = context.Restaurants.Single<Restaurant>(s => s.Id == id);
            return result;
        }

        public IList<Restaurant> List()
        {
            return context.Restaurants.ToList<Restaurant>();
        }

        public List<Restaurant> Search(string searchString)
        {
            var restaurants = new List<Restaurant>();
            if (!String.IsNullOrEmpty(searchString))
            {
                restaurants = context.Restaurants.Where(s => s.NameAr.Contains(searchString)).ToList();
            }

            return restaurants;
        }

        public void Update(int id, Restaurant entity)
        {
            throw new NotImplementedException();
        }
    }
}
