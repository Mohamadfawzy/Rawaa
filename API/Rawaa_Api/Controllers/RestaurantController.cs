using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Models;
using Rawaa_Api.Services;
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase 
    {
        
        private readonly IProvider<Restaurant> data;
        //IProvider<Restaurant> ad = new RestaurantData();

        public RestaurantController(IProvider<Restaurant> _restaurantData)
        {
            data = _restaurantData;
        }
        
        [HttpPost("create")]
        public IActionResult PostRestaurant([FromBody] Restaurant restaurant)
        {
            var result = data.Add(restaurant);
            return Ok(result);
        }

        [HttpGet("show_all")]
        public IActionResult GetAll()
        {
            return Ok(data.List());
        }

        [HttpGet("show/{id=1}")]
        public IActionResult GetId(int id)
        {
            return Ok(data.Find(1));
        }

        [HttpGet("search/{searchString}")]
        public IActionResult Search(string searchString="r")
        {
            return Ok(data.Search(searchString));
        }

    }
}
