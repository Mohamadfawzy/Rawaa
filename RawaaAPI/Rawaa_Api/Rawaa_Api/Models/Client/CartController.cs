using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Services.Client;

namespace Rawaa_Api.Models.Client
{
    [Route("{lang}/api/client/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartData data;
        public CartController()
        {
            data = new CartData();
        }

        // create Cart by user
        [HttpPost]
        public IActionResult PostSingle([FromBody] Cart model)
        {
            // check params
            if (model == null || model.CustomerId == 0 || model.DrinkId == 0 || model.ProductId == 0)
                return BadRequest(new ErrorClass("400", "is null feilds requird"));


            var result = data.Add(model);
            var res = data.Find(model);
            return Created("", res);
        }

        
        // get all product in cart of user
        [HttpGet("all/{userId}")]
        public IActionResult GetAll(int userId, string lang)
        {
            var result = data.List(userId, lang);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpDelete("{userId}/{productId}")]
        public IActionResult DeleteProductFromCart(int userId, int productId)
        {
            var result = data.Remove(userId, productId);
            if (!result)
            {
                return NoContent();
            }
            return Ok(result);
        }

    }
}
