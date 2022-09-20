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
        public IActionResult GetAll()
        {
            var result = data.List();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

       


    }
}
