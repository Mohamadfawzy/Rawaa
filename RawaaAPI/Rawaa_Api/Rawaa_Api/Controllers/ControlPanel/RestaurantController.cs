using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services;
using Rawaa_Api.Services.ControlPanel;
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Controllers.ControlPanel
{
    [Route("{lang}/api/cp/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantData data;

        // ctor
        public RestaurantController()
        {
            data = new RestaurantData();
        }

        [HttpPost]
        public IActionResult PostSingle([FromBody] Restaurant restaurant)
        {
            var result = data.Add(restaurant);
            return Created("", result);
        }
        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(data.Find(1));
        }

        [HttpGet("all")]
        public IActionResult GetAll(string lang)
        {
            var result = data.List(lang);
            return Ok(result);
        }


        [HttpGet("Search/{text}")]
        public IActionResult GetAllSearch(string text)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Restaurant model, string lang)
        {
            var result = data.Update(id, model, lang);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, string lang)
        {
            return Ok();
        }
    }
}
