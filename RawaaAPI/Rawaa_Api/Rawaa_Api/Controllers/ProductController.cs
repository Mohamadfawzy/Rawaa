using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Services;
using Rawaa_Api.Services.Interfaces;

namespace Rawaa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProvider<Product> data;
        FileProcessor fileProcessor;
        IWebHostEnvironment webHost;

        // Constructor ================================================================
        public ProductController(IProvider<Product> _data, IWebHostEnvironment webHost)
        {
            data = _data;
            this.webHost = webHost;
            this.fileProcessor = new FileProcessor(this.webHost);
        }


        [HttpPost("create")]
        public IActionResult PostProduct([FromForm] ImageUplod source, [FromForm] Product model)
        {
            if (String.IsNullOrEmpty(model.Title))
                return BadRequest(new { StatusCode = 400, Message = $"The {nameof(model.Title)} field is required." });

            var result = data.Add(model);
            fileProcessor.PostImage(source, result.Image);
            return CreatedAtAction(nameof(GetId), new { id = result.Id }, result); // id == param id in func GetIt
            //return Ok(result);
        }



        [HttpGet("show_all")]
        public IActionResult GetAll()
        {
            return Ok(data.List());
        }

        [HttpGet("show/{id=1}")]
        public IActionResult GetId(int id)
        {
            var res = data.Find(id);
            if (res != null)
            {
                return Ok(res);
            };
            //return Ok($"The Product id {id} not found");
            //return NotFound($"The Product id {id} not found");
            return NotFound(new { StatusCode = 404, Message = $"Not Found {nameof(ProductController)} by {id} " });

        }

        [HttpGet("search/{searchString}")]
        public IActionResult Search(string searchString = "r")
        {
            return Ok(data.Search(searchString));
        }
    }
}
