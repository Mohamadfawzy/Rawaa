using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rawaa_Api.Helper;
using Rawaa_Api.Models;
using Rawaa_Api.Services.Interfaces;
using System;

namespace Rawaa_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IProvider<Category> data;
        FileProcessor fileProcessor;
        IWebHostEnvironment webHost;

        // Constructor ================================================================
        public CategoryController(IProvider<Category> _data, IWebHostEnvironment webHost)
        {
            data = _data;
            this.webHost = webHost;
            this.fileProcessor = new FileProcessor(this.webHost);
        }

        [HttpPost("create")]
        public IActionResult PostSingle([FromForm] ImageUplod source, [FromForm] Category model)
        {
            if (String.IsNullOrEmpty(model.Title))
                return BadRequest(new { StatusCode = 400, Message = $"The {nameof(model.Title)} field is required." });

            var result = data.Add(model);
            var imageName = fileProcessor.PostImage(source, result.Image);
            //result.Image = imageName.Result;
            //CreatedAtAction(nameof(GetId), new { id = result.Id }, result); // id == param id in func GetIt
            return Ok( result);
            //return Ok(result);
        }



        [HttpGet("show_all")]
        public IActionResult GetAll()
        {
            return Ok(data.List());
        }



    }
}
