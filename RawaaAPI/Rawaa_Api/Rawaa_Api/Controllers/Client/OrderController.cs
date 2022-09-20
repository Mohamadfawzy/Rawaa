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

        // create order by user
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

        // cancel order by user
        [HttpPut("OrderStatus")]
        public IActionResult PutCancelOrder([FromBody] OrderStatusRequest state)
        {
            if(state.OrderStatus == 0 || state.OrderStatus >5)
            {
                return BadRequest(new ErrorClass("400", "you must insert order status and between 1-5 "));
            }
            var result = data.CancelOrder(state);
            if (result != null)
                return Ok(result);
            return BadRequest(new ErrorClass("400", "check your password or user not found"));
        }

        // get all order of user
        [HttpGet("all/{userId}")]
        public IActionResult GetAll(int userId)
        {
            var result = data.List(userId);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

       

        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id,string lang)
        {
            if (id < 1)
                return BadRequest(new ErrorClass("400", "id is invalid"));
            var res = data.OrderDetails(id,lang);
            if (res == null)
            {
                return NoContent();
            }
            return Ok(res);
        }

        [HttpGet("list-products/order/{id}")]
        public IActionResult GetProductsInOrder(int id, string lang)
        {
            if (id < 1)
                return BadRequest(new ErrorClass("400", "id is invalid"));
            var res = data.ProductsInOrder(id, lang);
            if (res == null)
            {
                return NoContent();
            }
            return Ok(res);
        }


    }
}
