using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Services.Client;

// this Controller for client 
namespace Rawaa_Api.Controllers.Client
{
    [Route("{lang}/api/client/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductClientData data;
        FileProcessor fileProcessor;
        IWebHostEnvironment webHost;

        // ctor client
        public ProductController()
        {
            data = new ProductClientData();
        }

        
        [HttpGet("{id}")]
        public IActionResult GetSingle(int id, string lang)
        {
            var result = data.Find(id, lang);
            if (result == null)
                return NotFound(new ErrorClass("404", $"the Product id: {id} not found"));
            return Ok(result);
        }

        [HttpGet("all/{categoryId}")]
        public IActionResult GetAll(string lang ,int categoryId)
        {
            var result = data.List(lang, categoryId);
            if (result == null)
                return NotFound(new ErrorClass("404", "There are no Products to display"));

            return Ok(result);
        }
        [HttpGet("max")]
        public IActionResult GetMax(string lang)
        {
            var result = data.Max(lang);
            if (result == null)
                return NotFound(new ErrorClass("404", "There are no Products to display"));

            return Ok(result);
        }

        [HttpGet("Search/{text}")]
        public IActionResult GetAllSearch(string text,string lang)
        {
            var result = data.Search(text, lang);
            if (result == null)
                return NotFound(new ErrorClass("404", "There are no categories to display"));
            return Ok(result);
        }

    }
}
