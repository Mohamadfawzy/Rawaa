using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.Entities;
using Rawaa_Api.Models;
using Rawaa_Api.Services.ControlPanel;
// cp
namespace Rawaa_Api.Controllers.ControlPanel
{
    [Route("{lang}/api/cp/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderData data;
        public OrderController()
        {
            data = new OrderData();
        }

        // get all order of user
        [HttpGet("totalPrice")]
        public IActionResult GetTotalPrice()
        {
            var result = data.TotalPrice();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // list of orders has stats
        [HttpGet("all")]
        public IActionResult GetList(int state,int pageNumber=1, int pageSize=10, int day = 1)
        {
            var result = data.List(state, pageNumber,pageSize,day);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // list of orders has stats
        [HttpGet("allJoinUserData")]
        public IActionResult GetListJoinUserData(int state, int pageNumber = 1, int pageSize = 10, int day = 1)
        {
            var result = data.ListJoinUserData(state, pageNumber, pageSize, day);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }


        // get order details with address
        [HttpGet("orderDetail/{id}")]
        public IActionResult GetOrderDetail(int id, string lang)
        {
            if (id < 1)
                return BadRequest(new ErrorClass("400", "id is invalid"));
            var res = data.OrderDetails(id, lang);
            if (res == null)
            {
                return NoContent();
            }
            return Ok(res);
        }

        // get all products in the order by order id
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

        // change status in  order by statff
        [HttpPut()]
        public IActionResult PutCancelOrder([FromBody] OrderStatusRequest model)
        {
            if (model.OrderStatus == 0 || model.OrderStatus > 5)
            {
                return BadRequest(new ErrorClass("400", "you must insert order status and between 1-5 "));
            }
            var result = data.UpdateOrder(model);
            if (result != null)
                return Ok(result);
            return BadRequest(new ErrorClass("400", "not found by id"));
        }

    }
}
