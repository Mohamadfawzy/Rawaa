using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models.ControlPanel;
using Rawaa_Api.Services.Client;
using Rawaa_Api.Services.Interfaces;

// this Controller for client

namespace Rawaa_Api.Controllers.Client
{
    [Route("{lang}/api/client/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryClientData data;
        
        // ctor client
        public CategoryController()
        {
            data = new CategoryClientData();
        }

        [HttpGet("all")]
        public IActionResult GetAll(string lang)
        {
            var result = data.List(lang);
            if (result == null)
                return NotFound(new ErrorClass("404", "There are no categories to display"));

            return Ok(result);
        }



    }


}
