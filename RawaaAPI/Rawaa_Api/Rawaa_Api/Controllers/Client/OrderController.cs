using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Services;
using Rawaa_Api.Services.Client;

namespace Rawaa_Api.Controllers.Client
{
    [Route("{lang}/api/client/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderData data;
        public OrderController()
        {
            data = new OrderData();
        }
        [HttpPost]
        public IActionResult PostSingle([FromBody] Order model)
        {
            // check params
            if (model == null)
                return BadRequest(new ErrorClass("400", "is null"));
            //var rs = CastClass.IsNullOrEmpty(new[] { model.CustomerId.ToString(), model.Quantity.ToString() });
            //if (rs)
            //    return BadRequest(new ErrorClass("400", "fields is requird"));
            
            // add
            var result = data.Add(model);
            return Created("", result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserRequest user)
        {
            
           return BadRequest(new ErrorClass("400", "email is not exist"));
           
            // add
            var result = data.Login(user);
            if (result == null)
            {
                return BadRequest(new ErrorClass("400", "Password or email is invalid"));
            }
            return Created("", result);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer user)
        {
            return BadRequest(new ErrorClass("400", "can not use this email"));
            
            var result = data.Update(id, user);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody] UserRequest user, int id)
        {
            var result = data.Delete(id, user.Password);
            if (result != null)
                return Ok(result);
            return BadRequest(new ErrorClass("400", "check your password or user not found"));
        }

        // end clint

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
