using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Services;

namespace Rawaa_Api.Controllers.Client
{
    [Route("api/client/[controller]")]
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {
        private readonly DeliveryAddressData data;
       
        public DeliveryAddressController()
        {
            data = new DeliveryAddressData();
        }
        
        [HttpPost]
        public IActionResult PostSingle([FromBody] DeliveryAddress model)
        {
            var result = data.Add(model);
            return Created("", result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = data.Delete(id);
            if (result != null)
                return Ok(result);
            return BadRequest(new ErrorClass("400", "check your password or user not found"));
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = data.List();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }


        [HttpGet("Search/{text}")]
        public IActionResult GetAllSearch(string text)
        {
            var result = data.Search(text);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}
