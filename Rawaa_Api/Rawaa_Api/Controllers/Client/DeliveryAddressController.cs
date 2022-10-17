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
            var res = CastClass.IsNullOrEmpty(new string[] { model.City, model.Governorate, model.ShortName, model.CustomerId.ToString() });
            if (res)
            {
                return BadRequest(new ErrorClass("400", "fields required"));
            }
            var result = data.Add(model);
            return Created("", result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = data.Delete(id);
            if (result == null)
                return BadRequest(new ErrorClass("400", "user or address not found"));

            if(result == true)
            {
                return BadRequest(new ErrorClass("400", "user or address not found"));
            }
            return Ok(new {isDelete = true});
        }

        [HttpGet("all/user/{id}")]
        public IActionResult GetAll(int id)
        {
            var result = data.List(id);
            if (result.Count<1)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            if (id < 1)
                return BadRequest(new ErrorClass("400", "id is invalid"));
            var res = data.Find(id);
            if (res == null)
            {
                return NoContent();
            }
            return Ok(res);
        }




        // not handel

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
