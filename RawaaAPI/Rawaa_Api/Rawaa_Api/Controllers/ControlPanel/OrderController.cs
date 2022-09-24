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


        // cancel order by user
        [HttpPut()]
        public IActionResult PutCancelOrder([FromBody] OrderStatusRequest state)
        {
            if (state.OrderStatus == 0 || state.OrderStatus > 5)
            {
                return BadRequest(new ErrorClass("400", "you must insert order status and between 1-5 "));
            }
            var result = data.UpdateOrder(state);
            if (result != null)
                return Ok(result);
            return BadRequest(new ErrorClass("400", "check your password or user not found"));
        }

    }
}
